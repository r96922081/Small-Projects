import pygame
import pygame.gfxdraw
import sys

def init():
    pygame.init()
    pygame.display.set_mode((800, 600))
    pygame.display.set_caption('Tank')
        
def game_loop():
    
    exit_game_loop = False

    while not exit_game_loop:
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                quit_game()
            elif event.type == pygame.KEYDOWN:
                if event.key == pygame.K_p:
                    pause_game()
                elif event.key == pygame.K_q:
                    quit_game()

def quit_game():
    pygame.quit()
    sys.exit()

init()
game_loop()


