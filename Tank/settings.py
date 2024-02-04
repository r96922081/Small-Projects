import pygame
import pygame.gfxdraw

# colors
white = pygame.Color(255, 255, 255)
black = pygame.Color(0, 0, 0)
green = pygame.Color(0,155,0)
red = pygame.Color(255, 88, 79)
gray = pygame.Color(169, 169, 169)
light_yellow = pygame.Color(255,255,0)
yellow = pygame.Color(200,200,0)

back_color = gray
fore_color = white
grid_line_color = white

player_color = green
enemy_color = red
wall_color = gray
mud_color = pygame.Color(255, 102, 102)

# sizes
block_size = 30
tank_size = block_size
border_size = block_size

display_width = 450
display_height  = 480

game_rect = pygame.Rect(block_size, block_size, display_width - 4 * block_size, display_height - 2 * block_size) 

game_width = 360
game_height = 420

# others
screen_surface = pygame.display.set_mode((display_width,display_height))
fps =120
map_file = 'map.txt'
