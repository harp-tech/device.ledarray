#include "fly_pit_boxes.h"
#include "i2c.h"

i2c_dev_t bus_expander;
uint16_t box_masks[8];

bool initialize_boxes(void)
{
   bool bus_expander_is_on_bus;
   
   bus_expander.data[0] = 0;
   bus_expander.data[1] = 0;
   
   for (uint8_t i = 0; i < 8; i++)
   {  
      bus_expander.add = 0x20 | i;
      box_masks[i] = 0;
      
      if (i == 0)
         bus_expander_is_on_bus = i2c0_wArray(&bus_expander, 2);
      else
         i2c0_wArray(&bus_expander, 2);
   }
   
   return bus_expander_is_on_bus;
}

void update_leds_on_box(uint8_t cmd)
{
   uint8_t flypad_index = (cmd & 0x1F) % 4;
   uint8_t bus_expander_low_address = (cmd & 0x1F) >> 2;
   
   bool led_is_on = cmd & (1 << 5) ? true : false;
   
   bool color_is_red =   (((cmd >> 6) & 0x03) == 1) ? true : false;
   bool color_is_green = (((cmd >> 6) & 0x03) == 2) ? true : false;
   bool color_is_blue =  (((cmd >> 6) & 0x03) == 3) ? true : false; 
   
   uint8_t flypad_bitmask = (color_is_red ? 0x01 : 0) | (color_is_green ? 0x02 : 0) | (color_is_blue ? 0x04 : 0) | (led_is_on ? 0x08 : 0);
   
   switch (flypad_index)
   {
      case 0:  box_masks[bus_expander_low_address] &= ~(0xF << 12);
               box_masks[bus_expander_low_address] |= (flypad_bitmask << 12);
               break;
               
      case 1:  box_masks[bus_expander_low_address] &= ~(0xF << 4);
               box_masks[bus_expander_low_address] |= (flypad_bitmask << 4);
               break;
      
      case 2:  box_masks[bus_expander_low_address] &= ~(0xF << 8);
               box_masks[bus_expander_low_address] |= (flypad_bitmask << 8);
               break;
      
      case 3:  box_masks[bus_expander_low_address] &= ~(0xF << 0);
               box_masks[bus_expander_low_address] |= (flypad_bitmask << 0);
               break;               
   }
   
   bus_expander.add = 0x20 | bus_expander_low_address;
   bus_expander.reg = box_masks[bus_expander_low_address] & 0x00FF;
   bus_expander.data[0] = box_masks[bus_expander_low_address] >> 8;

   i2c0_wArray(&bus_expander, 2);   
}