#ifndef _FLY_PIT_BOXES_
#define _FLY_PIT_BOXES_
#include "cpu.h"
#include <avr/io.h>

bool initialize_boxes(void);
void update_leds_on_box(uint8_t cmd);

#endif /* _FLY_PIT_BOXES_ */