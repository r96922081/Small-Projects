import pygame
import pygame.gfxdraw
import sys

from game_component import *
from settings import *
from scene import *

def init():
    pygame.init()
    pygame.display.set_caption('Tank')
        
def game_loop():
    
    exit_game_loop = False
    
    scene_mediator = scene_mediator_class(welcome_scene_class(), game_play_scene_class(), map_editor_scene_class())
    
    while not exit_game_loop:
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                quit_game()
            elif event.type == pygame.KEYDOWN:
                if event.key == pygame.K_p:
                    pause_game()
                elif event.key == pygame.K_q:
                    quit_game()   

            scene_mediator.current_scene.handle_event(event)

        scene_mediator.current_scene.draw()
        
def quit_game():
    pygame.quit()
    sys.exit()

init()
game_loop()


