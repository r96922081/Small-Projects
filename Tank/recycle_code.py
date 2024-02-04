import pygame
import pygame.gfxdraw

def pause_game():
    exit_while_loop = False
    
    while not exit_while_loop:
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                quit_game()
            elif event.type == pygame.KEYDOWN:
                if event.key == pygame.K_r:
                    exit_while_loop = True
                elif event.key == pygame.K_q:
                    quit_game()
                
        screen_surface.fill(white)
        display_multiline_message_in_center('Click R to resume, or Q to quit')
        pygame.display.update()
        pygame.time.Clock().tick(fps)

def show_welcome_message():
    exit_game_intro = False
    
    while not exit_game_intro:
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                quit_game()
            elif event.type == pygame.KEYDOWN:
                if event.key == pygame.K_s:
                    exit_game_intro = True
                elif event.key == pygame.K_q:
                    quit_game()
                
        screen_surface.fill(white)
        display_multiline_message_in_center('Welcome to Tank!\n\n\nPress S to start, P to pause, or Q to quit')
        pygame.display.update()
        pygame.time.Clock().tick(fps)

def show_lost_message():
    while True:
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                quit_game()
            elif event.type == pygame.KEYDOWN:
                if event.key == pygame.K_q:
                    quit_game()
                elif event.key == pygame.K_r:
                    reset_game()
                    return
                
        screen_surface.fill(white)
        display_multiline_message_in_center('Snake was dead, Press R to restart, or Q to quit')
        pygame.display.update()
        pygame.time.Clock().tick(fps)
