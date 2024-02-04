import javax.swing.*;
import java.awt.*;
import java.awt.event.KeyAdapter;
import java.awt.event.KeyEvent;
import java.util.ArrayList;
import java.util.TimerTask;

public class SnakeGame extends JPanel {

    static int width = 400;
    static int height = 400;
    static int size = 50;
    java.util.Timer timer;
    JFrame frame = new JFrame();
    Direction direction = Direction.E;
    Direction nextDirection = Direction.E;
    java.util.List<Pos> snakeBody = new ArrayList<>();
    Pos food;

    int score = 0;

    boolean dead = false;

    boolean inited = false;

    public SnakeGame() {
        frame.add(this);
        this.setPreferredSize(new Dimension(width, height));
        this.setBackground(Color.WHITE);
        this.setFocusable(true);

        frame.setTitle("Snake Game");
        frame.pack();
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.setResizable(false);
        frame.setVisible(true);

        Reset();

        this.addKeyListener(new KeyAdapter() {
            @Override
            public void keyPressed(KeyEvent e) {
                if (dead) {
                    if (e.getKeyCode() == KeyEvent.VK_ENTER) {
                        Reset();
                        return;
                    }
                }

                switch (e.getKeyCode()) {
                    case KeyEvent.VK_LEFT:
                        nextDirection = Direction.W;
                        break;
                    case KeyEvent.VK_RIGHT:
                        nextDirection = Direction.E;
                        break;
                    case KeyEvent.VK_UP:
                        nextDirection = Direction.N;
                        break;
                    case KeyEvent.VK_DOWN:
                        nextDirection = Direction.S;
                        break;
                }
            }
        });

        inited = true;
    }

    public static void main(String[] args) {
        //new Game();
        new SnakeGame();
    }

    public void Reset() {
        snakeBody.clear();

       /* snakeBody.add(new Pos(size * 6, 0));
        snakeBody.add(new Pos(size * 5, 0));
        snakeBody.add(new Pos(size * 4, 0));
        snakeBody.add(new Pos(size * 3, 0));
        snakeBody.add(new Pos(size * 2, 0));
        snakeBody.add(new Pos(size, 0));*/
        snakeBody.add(new Pos(0, 0));

        CreateFood();

        score = 0;
        dead = false;

        timer = new java.util.Timer();
        timer.schedule(new MyTimerTask(this), 0, 250);
    }

    private void SetDirection() {
        if (direction == Direction.N && nextDirection == Direction.S)
            return;
        if (direction == Direction.S && nextDirection == Direction.N)
            return;
        if (direction == Direction.W && nextDirection == Direction.E)
            return;
        if (direction == Direction.E && nextDirection == Direction.W)
            return;

        direction = nextDirection;
    }

    public void tick() {
        SetDirection();

        Pos newHead = snakeBody.get(0).clone();
        snakeBody.add(0, newHead);
        if (direction == Direction.N) {
            newHead.y -= size;
            if (newHead.y == -size)
                newHead.y = height - size;
        } else if (direction == Direction.S) {
            newHead.y += size;
            if (newHead.y == height)
                newHead.y = 0;
        } else if (direction == Direction.W) {
            newHead.x -= size;
            if (newHead.x == -size)
                newHead.x = width - size;
        } else if (direction == Direction.E) {
            newHead.x += size;
            if (newHead.x == width)
                newHead.x = 0;
        }

        if (DetectCollision()) {
            timer.cancel();
            dead = true;
            repaint();
            return;
        }

        if (newHead.x == food.x && newHead.y == food.y) {
            score++;
            CreateFood();
        } else {
            snakeBody.remove(snakeBody.size() - 1);
        }

        repaint();
    }

    public boolean DetectCollision() {
        for (int i = 0; i < snakeBody.size(); i++) {
            Pos p = snakeBody.get(i);
            for (int j = i + 1; j < snakeBody.size(); j++) {
                Pos p2 = snakeBody.get(j);
                if (p.x == p2.x && p.y == p2.y) {
                    return true;
                }
            }
        }

        return false;
    }

    public void CreateFood() {
        java.util.List<Pos> available = new ArrayList<>();
        for (int x = 0; x < width; x += size) {
            for (int y = 0; y < height; y += size) {
                boolean ok = true;
                for (Pos p : snakeBody) {
                    if (x == p.x && y == p.y) {
                        ok = false;
                        break;
                    }
                }

                if (ok)
                    available.add(new Pos(x, y));
            }
        }

        if (!available.isEmpty()) {
            int index = (int) (Math.random() * available.size());
            this.food = available.get(index);
        } else {
            // win
        }

    }

    @Override
    protected void paintComponent(java.awt.Graphics g) {
        if (!inited)
            return;

        super.paintComponent(g);

        g.setColor(Color.DARK_GRAY);
        for (Pos p : snakeBody) {
            g.fillRect(p.x, p.y, size, size);
        }

        g.setColor(Color.green);
        g.fillRect(food.x, food.y, size, size);

        if (dead) {
            String scoreText = String.format("Dead. Score: %d. Press Enter to restart", score);
            g.setColor(Color.red);
            g.drawString(scoreText, (width - getFontMetrics(g.getFont()).stringWidth(scoreText)) / 2, height / 2);
        }
    }

    public static enum Direction {
        N, S, W, E
    }

    public static class Pos {
        public int x = 0;
        public int y = 0;

        public Pos(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public Pos clone() {
            return new Pos(x, y);
        }
    }

    public static class MyTimerTask extends TimerTask {
        SnakeGame s;

        public MyTimerTask(SnakeGame s) {
            this.s = s;

        }

        @Override
        public void run() {
            s.tick();
        }
    }
}
