import pygame

from settings import *
from game_component import *

class scene_class:
    def __init__(self):
        self.scene_mediator = None
        self.components = []
  
    def handle_event(self, event):
        for component in self.components:
            component.handle_event(event)
        
    def draw(self):
        for component in self.components:
            component.draw()
 
    def active(self):
        raise Exception('active')
 
    def inactive(self):
        raise Exception('inactive')
 
    def set_scene_mediator(self, scene_mediator):
        self.scene_mediator = scene_mediator
            
    def draw_line(self, block_size):
        x_end = game_rect.width // block_size + 1
        y_end = game_rect.height // block_size + 1    
        
        for i in range(y_end):
            pygame.draw.line(screen_surface, grid_line_color, (game_rect.left,  game_rect.top + i * block_size), (game_rect.left + game_rect.width,  game_rect.top + i * block_size))
            
        for i in range(x_end):
            pygame.draw.line(screen_surface, grid_line_color, (game_rect.left + i * block_size, game_rect.top), (game_rect.left + i * block_size, game_rect.top + game_rect.height))
            
    def draw_game_rect(self):
        pygame.draw.rect(screen_surface, black, pygame.Rect(block_size, block_size, display_width - 4 * block_size, display_height - 2 * block_size))

class welcome_scene_class(scene_class):
    def __init__(self):
        super().__init__()
        self.play_button = button_class('Play', 'welcome_scene_play', display_width // 7, display_height // 2, block_size * 3, block_size // 2 * 3, 30, green, light_yellow)
        self.edit_button = button_class('Edit Map', 'welcome_scene_edit_map', display_width // 7 * 4, display_height // 2, block_size * 5, block_size // 2 * 3, 30, yellow, light_yellow)
        
        self.components.append(self.play_button)
        self.components.append(self.edit_button)
        
    def set_scene_mediator(self, scene_mediator):
        super().set_scene_mediator(scene_mediator)
        self.play_button.add_click_listener(scene_mediator)
        self.edit_button.add_click_listener(scene_mediator)
        
    def draw(self):
        screen_surface.fill(back_color)
        
        welcome_font = pygame.font.SysFont("calibri", 30)
        welcome_surface = welcome_font.render('Welcome to Tank', True, white)
        welcome_width = welcome_surface.get_rect().width
        welcome_height = welcome_surface.get_rect().height
        screen_surface.blit(welcome_surface, (display_width // 2 - welcome_width // 2,  display_height // 4, welcome_width, welcome_height))

        for component in self.components:
            component.draw()

        pygame.display.update()
        pygame.time.Clock().tick(fps)
        
    def active(self):
        pass
 
    def inactive(self):
        pass

class game_play_scene_class(scene_class):
    def __init__(self):
        super().__init__()
        self.map = map_class()
        self.ok_button = button_class('Return', 'game_play_scene_return', display_width - block_size * 2, display_height - border_size * 2, block_size * 1.9, block_size, 18, yellow, light_yellow)
        self.components.append(self.ok_button)
        self.components.append(self.map)
 
    def handle_event(self, event):
        super().handle_event(event)
        
    def set_scene_mediator(self, scene_mediator):
        super().set_scene_mediator(scene_mediator)
        self.ok_button.add_click_listener(scene_mediator)
        
    def draw(self):
        self.map.step()
        screen_surface.fill(back_color)
        self.draw_game_rect()

        for component in self.components:
            component.draw()
        
        pygame.display.update()
        pygame.time.Clock().tick(fps)        
        
    def active(self):
        self.map.units.clear()
        map_file_handler().load_map(self.map, map_file)
 
    def inactive(self):
        pass

class map_saver:
    def __init__(self, file_name, map):
        self.file_name = file_name
        self.map = map
    
    def handle_button_event(self, event):
        if event == 'map_editor_scene_ok click':
            map_file_handler().save_map(self.map, map_file)

class map_editor_scene_class(scene_class):
    def __init__(self):
        super().__init__()
        self.map = map_class()
        self.select_unit = None
        self.draging = False
        self.previous_mouse_x = 0
        self.previous_mouse_y = 0
        self.ok_button = button_class('OK', 'map_editor_scene_ok', display_width - block_size * 3 , display_height - border_size * 2, block_size * 2, block_size // 2 * 3, 20, yellow, light_yellow)
        self.ok_button.add_click_listener(map_saver(map_file, self.map))

        self.player_tank_button = tank_button_class('player_tank_button', game_rect.left + game_rect.width + 10, game_rect.top, tank_size, tank_size, player_color, 'up')
        self.player_tank_button.add_click_listener(self)
        
        self.enemy_tank_button = tank_button_class('enemy_tank_button', game_rect.left + game_rect.width + 20 + tank_size, game_rect.top, tank_size, tank_size, enemy_color, 'down')
        self.enemy_tank_button.add_click_listener(self)
   
        self.components.append(self.map)
        self.components.append(self.ok_button)
        self.components.append(self.player_tank_button)
        self.components.append(self.enemy_tank_button)
        
        self.handle_event_components = []
        self.handle_event_components.append(self.ok_button)
        self.handle_event_components.append(self.player_tank_button)
        self.handle_event_components.append(self.enemy_tank_button)
        
    def set_scene_mediator(self, scene_mediator):
        super().set_scene_mediator(scene_mediator)
        self.ok_button.add_click_listener(scene_mediator)
        
    def dot_in_rect(self, x, y, left, top, width, height):
        if left < x and x < left + width and top < y and y < top + height:
            return True
        
    def handle_button_event(self, event):
        if event == 'player_tank_button click':
            tank_factory = tank_factory_class(self.map)
            tank_factory.create_player_tank(screen_surface, player_color, enemy_color, self.player_tank_button.left, self.player_tank_button.top, tank_size, tank_size)
            
        elif event == 'enemy_tank_button click':
            tank_factory = tank_factory_class(self.map)
            tank_factory.create_enemy_tank(screen_surface, player_color, enemy_color, self.enemy_tank_button.left, self.enemy_tank_button.top, tank_size, tank_size)

    def handle_event(self, event):
        for c in self.handle_event_components:
            c.handle_event(event)
        
        if event.type == pygame.MOUSEBUTTONDOWN:
            click = pygame.mouse.get_pressed()
            pos = pygame.mouse.get_pos() 
            if click[0] == 1:
                self.select_unit = None
                for unit in self.map.units:
                    if self.dot_in_rect(pos[0], pos[1], unit.left, unit.top, unit.width, unit.height):
                        self.select_unit = unit
                        self.draging = True
                        break
                        
        elif event.type == pygame.MOUSEBUTTONUP:
            if self.select_unit != None:
                left = self.select_unit.left
                top = self.select_unit.top
                if left < game_rect.left or game_rect.left + game_rect.width < left + self.select_unit.width or \
                    top < game_rect.top or game_rect.top + game_rect.height < top + self.select_unit.height:                
                    self.map.remove(self.select_unit)
                    self.select_unit = None
            
            self.draging = False
            
        elif event.type == pygame.MOUSEMOTION:
            pos = pygame.mouse.get_pos()
            x = pos[0]
            y = pos[1]
            if self.draging:
                self.select_unit.left += x - self.previous_mouse_x
                self.select_unit.top += y - self.previous_mouse_y
                
            self.previous_mouse_x = x
            self.previous_mouse_y = y
            
        elif event.type == pygame.KEYDOWN:
            if event.key == pygame.K_DELETE:
                self.map.remove(self.select_unit)
                self.select_unit = None
        
    def draw_select_rect(self):
        if self.select_unit == None:
            return
            
        thick = 2
        pygame.draw.line(screen_surface, white, (self.select_unit.left, self.select_unit.top), (self.select_unit.left + self.select_unit.width, self.select_unit.top), thick)
        pygame.draw.line(screen_surface, white, (self.select_unit.left, self.select_unit.top + self.select_unit.height), (self.select_unit.left + self.select_unit.width, self.select_unit.top + self.select_unit.height), thick)
        pygame.draw.line(screen_surface, white, (self.select_unit.left, self.select_unit.top), (self.select_unit.left, self.select_unit.top + self.select_unit.height), thick)
        pygame.draw.line(screen_surface, white, (self.select_unit.left + self.select_unit.width, self.select_unit.top), (self.select_unit.left + self.select_unit.width, self.select_unit.top + self.select_unit.height), thick)
        
    def draw(self):
        screen_surface.fill(back_color)
        self.draw_game_rect()
        self.draw_line(block_size)
        
        super().draw()

        self.draw_select_rect()
            
        pygame.display.update()
        pygame.time.Clock().tick(fps)      
        
    def active(self):
        self.map.units.clear()
        map_file_handler().load_map(self.map, map_file)
 
    def inactive(self):
        pass

class scene_mediator_class(button_class):
    def __init__(self, welcome_scene, game_play_scene, map_editor_scene):
        self.game_play_scene = game_play_scene
        self.welcome_scene = welcome_scene
        self.map_editor_scene = map_editor_scene        
        self.all_scenes = [game_play_scene, welcome_scene, map_editor_scene]
        
        for scene in self.all_scenes:
            scene.set_scene_mediator(self)
        
        self.current_scene = welcome_scene
        
    def handle_button_event(self, event):
        if event == 'welcome_scene_play click':
            self.current_scene = self.game_play_scene
        elif event == 'welcome_scene_edit_map click':
            self.current_scene = self.map_editor_scene
        elif event == 'map_editor_scene_ok click':
            self.current_scene = self.welcome_scene
        elif event == 'game_play_scene_return click':
            self.current_scene = self.welcome_scene
            
        self.current_scene.active()
