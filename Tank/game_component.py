import pygame
import random
import sys

from settings import *

class unit:
    def __init__(self, map, screen, color, id, left, top, width, height):
        self.screen = screen
        self.map = map
        self.left = left
        self.top = top
        self.width = width
        self.height = height
        self.color = color
        self.location_blocks = []
        self.id = id
        map.add(self)
        
    def step(self):
        pass
        
    def draw(self):
        pass
        
    def handle_event(self, event):
        pass
        
class default_step_handler:
    def __init__(self, map):
        self.map = map
        self.step_counter = 0  
        
    def handle_step(self, tank):
        self.step_counter += 1
        if self.step_counter % tank.speed != 0:
            return
            
        if self.step_counter == sys.maxsize:
            self.step_counter = 0
        
        y_diff = 0
        x_diff = 0
        move_distance = 1
        if tank.direction == 'up':
            y_diff = -move_distance
        elif tank.direction == 'down':
            y_diff = move_distance
        elif tank.direction == 'left':
            x_diff = -move_distance
        elif tank.direction == 'right':
            x_diff = move_distance
        
        left = tank.left + x_diff
        top = tank.top + y_diff

        #if top < 0 or top + tank.height > self.map.height or left < 0 or left + tank.width > self.map.width:
        #    return
        
        units = self.map.get_unit(left, top, tank.width, tank.height)
        units.remove(tank)
       
        if len(units) != 0:
            return
            
        tank.left = left
        tank.top = top
        
class player_tank_step_handler(default_step_handler):
    def __init__(self, map):
        super().__init__(map)
    
    def handle_step(self, tank):       
        super().handle_step(tank)
        
class enemy_tank_step_handler(default_step_handler):
    def __init__(self, map):
        super().__init__(map)
    
    def handle_step(self, tank): 
        super().handle_step(tank)
        if random.randint(0, tank.speed * 20) == 0:
            tank.set_direction(random.choice(['up', 'down', 'left', 'right']))
            
        if random.randint(0, tank.speed * 100) == 0:
            tank.fire()
        
class default_destroy_checker:
    def __init__(self, map):
        self.map = map
    
    def check(self, unit):
        pass
        
class player_destroy_checker(default_destroy_checker):
    def __init__(self, map):
        super().__init__(map)
    
    def check(self, unit):
        if (type(unit) is tank and unit.id != 'player_tank') or (type(unit) is bullet_class):
            return True
        else:
            return False

class enemy_destroy_checker(default_destroy_checker):
    def __init__(self, map):
        super().__init__(map)
    
    def check(self, unit):
        if (type(unit) is tank and unit.id == 'player_tank') or (type(unit) is bullet_class):
            return True
        else:
            return False
        
class default_event_handler:
    def handle_event(self, unit, event):
        pass
        
class player_tank_event_handler(default_event_handler):
    def __init__(self):
        self.key_down_dirs = []
        
    def reset(self):
        self.key_down_dirs.clear()
    
    def handle_event(self, tank, event): 
        if event.type == pygame.KEYDOWN:
            if event.key == pygame.K_UP:
                tank.set_direction('up')
                self.key_down_dirs.append(event.key)
                tank.moving = True
            elif event.key == pygame.K_DOWN:
                tank.set_direction('down')
                self.key_down_dirs.append(event.key)
                tank.moving = True
            elif event.key == pygame.K_LEFT:
                tank.set_direction('left')
                self.key_down_dirs.append(event.key)
                tank.moving = True
            elif event.key == pygame.K_RIGHT:
                tank.set_direction('right')
                self.key_down_dirs.append(event.key)
                tank.moving = True
            elif event.key == pygame.K_SPACE:
                tank.fire()
        elif event.type == pygame.KEYUP:
            if event.key == pygame.K_UP or event.key == pygame.K_DOWN or event.key == pygame.K_LEFT or event.key == pygame.K_RIGHT:
                for i in range(len(self.key_down_dirs)):
                    if self.key_down_dirs[i] == event.key:
                        del self.key_down_dirs[i]
                        break
            
        if len(self.key_down_dirs) == 0:
            tank.moving = False
        else:
            if self.key_down_dirs[-1] == pygame.K_UP:
                tank.set_direction('up')
            elif self.key_down_dirs[-1] == pygame.K_DOWN:
                tank.set_direction('down')
            elif self.key_down_dirs[-1] == pygame.K_LEFT:
                tank.set_direction('left')
            elif self.key_down_dirs[-1] == pygame.K_RIGHT:
                tank.set_direction('right')              
        
class draw_class:
    def draw_tank(self, left, top, width, height, direction, color):        
        if direction == 'up':
            pygame.draw.rect(screen_surface, color, [left, top + int(width / 15 * 6), width, int(height / 15 * 9)])
        elif direction == 'down':
            pygame.draw.rect(screen_surface, color, [left, top , width, int(height / 15 * 9)])
        elif direction == 'left':
            pygame.draw.rect(screen_surface, color, [left + int(width / 15 * 6), top, int(width / 15 * 9), height])
        elif direction == 'right':
            pygame.draw.rect(screen_surface, color, [left, top, int(width / 15 * 9), height])

        pygame.draw.circle(screen_surface, color, (left + width // 2, top + height // 2),  width // 10 * 3)
            
        gun_thick = 4
        pygame.draw.line(screen_surface, color, (left, top + height // 2), (left + width - 1, top + height // 2), gun_thick)
        pygame.draw.line(screen_surface, color, (left + width // 2 , top), (left + width // 2 , top + height - 1), gun_thick)
            
    
class tank(unit):
    def __init__(self, map, screen, color, id, left, top, width, height):
        super().__init__(map, screen, color, id, left, top, width, height)
        self.direction = 'up'
        self.event_handler = default_event_handler()
        self.step_handler = default_step_handler(self.map)
        self.moving = False
        self.destroy_checker = default_destroy_checker(self.map)
        self.speed = 1
        
    def set_direction(self, direction):
        self.direction = direction
        
    def draw(self):
        draw_class().draw_tank(self.left, self.top, self.width, self.height, self.direction, self.color)
        
    def set_event_handler(self, event_handler):
        self.event_handler = event_handler
        
    def handle_event(self, event):
        self.event_handler.handle_event(self, event)
        
    def set_step_handler(self, step_handler):
        self.step_handler = step_handler

    # 1 is faster than 2
    def set_speed(self, speed):
        self.speed = speed
        
    def step(self):
        if self.moving:
            self.step_handler.handle_step(self)
            
    def set_destroy_handler(self, destroy_handler):
        self.destroy_handler = destroy_handler
            
    def fire(self):
        bullet_factory_class(self.map).create_bullet_1(self)

class tank_factory_class:
    def __init__(self, map):
        self.map = map
        
    def create_player_tank(self, screen, player_color, enemy_color, left, top, width, height):
        t = tank(self.map, screen, player_color, 'player_tank', left, top, width, height)
        t.set_event_handler(player_tank_event_handler())
        t.set_step_handler(player_tank_step_handler(self.map))
        t.set_speed(1)
        t.set_direction('up')
        t.set_destroy_handler(player_destroy_checker(self.map))
        return t
        
    def create_enemy_tank(self, screen, player_color,enemy_color, left, top, width, height):
        t = tank(self.map, screen, enemy_color, 'enemy_tank', left, top, width, height)
        t.set_step_handler(enemy_tank_step_handler(self.map))
        t.set_speed(6)
        t.moving = True
        t.set_direction('down')
        t.set_destroy_handler(enemy_destroy_checker(self.map))
        return t
       
class bullet_factory_class:
    def __init__(self, map):
        self.map = map    
    
    def create_bullet_1(self, tank):
        y = tank.top + tank.height // 2
        x = tank.left + tank.width // 2
        
        if tank.direction == 'up':
            y = tank.top
        elif tank.direction == 'down':
            y = tank.top + tank.height
        elif tank.direction == 'left':
            x = tank.left
        else:
            x = tank.left + tank.width
        
        bullet_length = 6
        bullet_width = 20
        bullet_speed = tank.speed
        bullet_move_distance = 2        
        return bullet_class(self.map, tank.screen, tank.color, tank.direction, 'player_bullet', x, y,  bullet_length, bullet_width, bullet_speed, bullet_move_distance, tank.destroy_handler)

class bullet_class(unit):
    def __init__(self, map, screen, color, direction, id, host_x, host_y, bullet_length, bullet_width, speed, move_distance, destroy_checker):
        self.direction = direction
        self.destroy_checker = destroy_checker
        self.speed = speed
        self.step_counter = 0
        self.move_distance = move_distance
        self.bullet_width = bullet_width
        
        if direction == 'up' or direction == 'down':
            height = bullet_length
            width = bullet_width
            if direction == 'up':
                left = host_x - bullet_width // 2
                top = host_y - bullet_length
            else:
                left = host_x - bullet_width // 2
                top = host_y
        else:
            width = bullet_length
            height = bullet_width
            if direction == 'left':
                left = host_x - bullet_length
                top = host_y - bullet_width // 2
            else:
                left = host_x
                top = host_y - bullet_width // 2
        
        super().__init__(map, screen, color, id, left, top, width, height)
        
    def draw(self):
        bullet_thick = 3
        if self.direction == 'up' or self.direction == 'down':
            pygame.draw.line(self.screen, self.color, (self.left + self.width // 2, self.top), (self.left + self.width // 2, self.top + self.height), bullet_thick)
        else:
            pygame.draw.line(self.screen, self.color, (self.left, self.top + self.height // 2), (self.left + self.width, self.top + self.height // 2), bullet_thick)
            
    def step(self):
        self.step_counter += 1
        if self.step_counter % self.speed != 0:
            return
            
        if self.step_counter == sys.maxsize:
            self.step_counter = 0        

        top = self.top
        left = self.left
        
        if self.direction == 'up':
            top -= self.move_distance
        elif self.direction == 'down':
            top += self.move_distance
        elif self.direction == 'right':
            left += self.move_distance
        elif self.direction == 'left':
            left -= self.move_distance

        
        units = self.map.get_unit(self.left, self.top, self.width, self.height)
        units.remove(self)
        self.left = left
        self.top = top
        
        if len(units) != 0:
            self.map.remove(self)
            for u in units:
                if self.destroy_checker.check(u) == True:
                    self.map.remove(u)

class regular_unit_class(unit):
    def __init__(self, map, screen, color, id, left, top, width, height):
        super().__init__(map, screen, color, id, left, top, width, height)
        
    def draw(self):
        pygame.draw.rect(self.screen, self.color, [self.left, self.top, self.width, self.height])
       
class map_class:
    def __init__(self):
        self.units = []

    def remove(self,unit):
        self.units.remove(unit)

    def add(self, unit):
        self.units.append(unit)
        
    def get_unit(self, left, top, width, height):
        right = left + width
        bottom = top + height
        
        ret = []
        for u in self.units:
            left2 = u.left
            top2 = u.top
            right2= u.left + u.width
            bottom2 = u.top + u.height
            if right2 > left and left2 < right and top2 < bottom and bottom2 > top:
                ret.append(u)

        return ret
        
    def step(self):
        for unit in self.units:
            unit.step()
            
    def draw(self):
        for unit in self.units:
            unit.draw()
            
    def handle_event(self, event):
        for unit in self.units:
            unit.handle_event(event)
            
class map_file_handler:
    def load_map(self, map, map_file_path):
        with open(map_file_path) as map_file:
            tank_factory = tank_factory_class(map)        
            
            for line in map_file:
                line = line.strip()
                if line.startswith('#'):
                    continue
                    
                if line == '':
                    continue
                    
                units = line.split(';')
                
                for unit in units:
                    if unit == '':
                        continue
                        
                    detail = unit.split(',')
                    type = detail[0].strip()
                    id = detail[1].strip()
                    left = int(detail[2].strip())
                    top = int(detail[3].strip())
                    
                    if type == "<class 'game_component.tank'>":
                        if id == 'player_tank':
                            tank_factory.create_player_tank(screen_surface, player_color, enemy_color, left, top, tank_size, tank_size)
                        elif id == 'enemy_tank':
                            tank_factory.create_enemy_tank(screen_surface, player_color, enemy_color, left, top, tank_size, tank_size)
                            
    def save_map(self, map, map_file_path):
        f = open(map_file_path, 'w')
        for unit in map.units:
            f.write(str(type(unit)))
            f.write(', ' + unit.id)
            f.write(', ' + str(unit.left))
            f.write(', ' + str(unit.top))
            f.write(';\n')
            
        f.flush()
        f.close()


class button_class:
    def __init__(self, text, id, left, top, width, height, font_size,  inactive_color, active_color):
        self.text = text
        self.id = id
        self.inactive_color = inactive_color
        self.active_color = active_color
        self.back_color = inactive_color
        self.button_font = pygame.font.SysFont("calibri", font_size)

        self.left = left
        self.top = top
        self.width = width
        self.height = height
        
        self.active_pressed = False
        
        self.click_listeners = []
        
    def add_click_listener(self, listener):
        self.click_listeners.append(listener)
        
    def handle_event(self, event):
        pos = pygame.mouse.get_pos() 
        click = pygame.mouse.get_pressed()
        if click[0] == 0:
            self.active_pressed = False
        
        if self.left < pos[0] and pos[0] < self.left + self.width and \
                self.top < pos[1] and pos[1] < self.top + self.height \
                and not self.active_pressed:
            self.back_color = self.active_color
            if click[0] == 1:
                for click_listener in self.click_listeners:
                    click_listener.handle_button_event(self.id + ' click')
                self.active_pressed = True
                
        else:
            self.back_color = self.inactive_color

    def draw(self):
        text_surface = self.button_font.render(self.text, True, fore_color)
        pygame.draw.rect(screen_surface, self.back_color, (self.left,  self.top, self.width, self.height))
        
        # text align is center
        screen_surface.blit(text_surface, (self.left + self.width // 2 - text_surface.get_rect().width // 2, self.top + self.height // 2 - text_surface.get_rect().height // 2, text_surface.get_rect().width, text_surface.get_rect().height))

class tank_button_class(button_class):
    def __init__(self, id, left, top, width, height,  tank_color, direction):
        super().__init__('', id, left, top, width, height, 0,  None, None)
        self.tank_color = tank_color
        self.direction = direction
        
    def draw(self):
        draw_class().draw_tank(self.left, self.top, self.width, self.height, self.direction, self.tank_color)
