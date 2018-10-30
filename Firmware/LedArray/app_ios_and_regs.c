#include <avr/io.h>
#include "hwbp_core_types.h"
#include "app_ios_and_regs.h"

/************************************************************************/
/* Configure and initialize IOs                                         */
/************************************************************************/
void init_ios(void)
{	/* Configure input pins */
	io_pin2in(&PORTA, 0, PULL_IO_TRISTATE, SENSE_IO_EDGES_BOTH);         // VERSION0
	io_pin2in(&PORTA, 3, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // VERSION1
	io_pin2in(&PORTD, 4, PULL_IO_TRISTATE, SENSE_IO_EDGES_BOTH);         // IN0
	io_pin2in(&PORTD, 3, PULL_IO_TRISTATE, SENSE_IO_EDGES_BOTH);         // IN1

	/* Configure input interrupts */
	io_set_int(&PORTD, INT_LEVEL_LOW, 0, (1<<4), false);                 // IN0
	io_set_int(&PORTD, INT_LEVEL_LOW, 1, (1<<3), false);                 // IN1

	/* Configure output pins */
	io_pin2out(&PORTC, 0, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // LED0_TRANSISTOR
	io_pin2out(&PORTC, 1, OUT_IO_DIGITAL, IN_EN_IO_DIS);                 // BOARD_LED1
	io_pin2out(&PORTC, 5, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // LED0_PWR_ON
	io_pin2out(&PORTC, 6, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // LED1_PWR_ON
	io_pin2out(&PORTD, 0, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // LED1_TRANSISTOR
	io_pin2out(&PORTD, 1, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // AUX1
	io_pin2out(&PORTD, 2, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // AUX0
	io_pin2out(&PORTC, 4, OUT_IO_DIGITAL, IN_EN_IO_DIS);                 // BOARD_LED0
	io_pin2out(&PORTD, 7, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // OUT0
	io_pin2out(&PORTA, 2, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // OUT1

	/* Initialize output pins */
	clr_LED0_TRANSISTOR;
	clr_BOARD_LED1;
	clr_LED0_PWR_ON;
	clr_LED1_PWR_ON;
	clr_LED1_TRANSISTOR;
	clr_AUX1;
	clr_AUX0;
	clr_BOARD_LED0;
	clr_OUT0;
	clr_OUT1;
}

/************************************************************************/
/* Registers' stuff                                                     */
/************************************************************************/
AppRegs app_regs;

uint8_t app_regs_type[] = {
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_FLOAT,
	TYPE_FLOAT,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_FLOAT,
	TYPE_FLOAT,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_FLOAT,
	TYPE_FLOAT,
	TYPE_FLOAT,
	TYPE_FLOAT,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8
};

uint16_t app_regs_n_elements[] = {
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1
};

uint8_t *app_regs_pointer[] = {
	(uint8_t*)(&app_regs.REG_POWER_EN),
	(uint8_t*)(&app_regs.REG_LED_BEHAVING),
	(uint8_t*)(&app_regs.REG_LED_ON),
	(uint8_t*)(&app_regs.REG_IN_STATE),
	(uint8_t*)(&app_regs.REG_OUT_CONFIGURATION),
	(uint8_t*)(&app_regs.REG_IN_CONFIGURATION),
	(uint8_t*)(&app_regs.REG_LED_CONFIGURATION),
	(uint8_t*)(&app_regs.REG_LED0_SUPPLY_PWR_CONF),
	(uint8_t*)(&app_regs.REG_LED1_SUPPLY_PWR_CONF),
	(uint8_t*)(&app_regs.REG_LED0_PWM_FREQ),
	(uint8_t*)(&app_regs.REG_LED0_PWM_DCYCLE),
	(uint8_t*)(&app_regs.REG_LED0_PWM_PULSES),
	(uint8_t*)(&app_regs.REG_LED0_INTERVAL_ON),
	(uint8_t*)(&app_regs.REG_LED0_INTERVAL_OFF),
	(uint8_t*)(&app_regs.REG_LED0_INTERVAL_PULSES),
	(uint8_t*)(&app_regs.REG_LED0_INTERVAL_TAIL),
	(uint8_t*)(&app_regs.REG_LED0_INTERVAL_REPS),
	(uint8_t*)(&app_regs.REG_LED1_PWM_FREQ),
	(uint8_t*)(&app_regs.REG_LED1_PWM_DCYCLE),
	(uint8_t*)(&app_regs.REG_LED1_PWM_PULSES),
	(uint8_t*)(&app_regs.REG_LED1_INTERVAL_ON),
	(uint8_t*)(&app_regs.REG_LED1_INTERVAL_OFF),
	(uint8_t*)(&app_regs.REG_LED1_INTERVAL_PULSES),
	(uint8_t*)(&app_regs.REG_LED1_INTERVAL_TAIL),
	(uint8_t*)(&app_regs.REG_LED1_INTERVAL_REPS),
	(uint8_t*)(&app_regs.REG_LED0_PWM_FREQ_REAL),
	(uint8_t*)(&app_regs.REG_LED0_PWM_DCYCLE_REAL),
	(uint8_t*)(&app_regs.REG_LED1_PWM_FREQ_REAL),
	(uint8_t*)(&app_regs.REG_LED1_PWM_DCYCLE_REAL),
	(uint8_t*)(&app_regs.REG_AUX_DIG_OUT),
	(uint8_t*)(&app_regs.REG_AUX_SUPPLY_PWR_CONF),
	(uint8_t*)(&app_regs.REG_OUT_STATE),
	(uint8_t*)(&app_regs.REG_DUMMY0),
	(uint8_t*)(&app_regs.REG_EVNT_ENABLE)
};