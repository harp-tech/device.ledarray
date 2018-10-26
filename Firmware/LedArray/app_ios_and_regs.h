#ifndef _APP_IOS_AND_REGS_H_
#define _APP_IOS_AND_REGS_H_
#include "cpu.h"

void init_ios(void);
/************************************************************************/
/* Definition of input pins                                             */
/************************************************************************/
// VERSION0               Description: Check hardware version
// VERSION1               Description: Check hardware version
// IN0                    Description: Board's input IN0
// IN1                    Description: Board's input IN1

#define read_VERSION0 read_io(PORTA, 0)         // VERSION0
#define read_VERSION1 read_io(PORTA, 3)         // VERSION1
#define read_IN0 read_io(PORTD, 4)              // IN0
#define read_IN1 read_io(PORTD, 3)              // IN1

/************************************************************************/
/* Definition of output pins                                            */
/************************************************************************/
// LED0_TRANSISTOR        Description: Controls transistor of LED0
// BOARD_LED1             Description: Board's LED1
// LED0_PWR_ON            Description: Enables power for LED0
// LED1_PWR_ON            Description: Enables power for LED1
// LED1_TRANSISTOR        Description: Controls transistor of LED1
// AUX1                   Description: Dummy digital output
// AUX0                   Description: Dummy digital output
// BOARD_LED0             Description: Board's LED0
// OUT0                   Description: Board's output OUT0
// OUT1                   Description: Board's output OUT1

/* LED0_TRANSISTOR */
#define set_LED0_TRANSISTOR set_io(PORTC, 0)
#define clr_LED0_TRANSISTOR clear_io(PORTC, 0)
#define tgl_LED0_TRANSISTOR toggle_io(PORTC, 0)
#define read_LED0_TRANSISTOR read_io(PORTC, 0)

/* BOARD_LED1 */
#define set_BOARD_LED1 set_io(PORTC, 1)
#define clr_BOARD_LED1 clear_io(PORTC, 1)
#define tgl_BOARD_LED1 toggle_io(PORTC, 1)
#define read_BOARD_LED1 read_io(PORTC, 1)

/* LED0_PWR_ON */
#define set_LED0_PWR_ON set_io(PORTC, 5)
#define clr_LED0_PWR_ON clear_io(PORTC, 5)
#define tgl_LED0_PWR_ON toggle_io(PORTC, 5)
#define read_LED0_PWR_ON read_io(PORTC, 5)

/* LED1_PWR_ON */
#define set_LED1_PWR_ON set_io(PORTC, 6)
#define clr_LED1_PWR_ON clear_io(PORTC, 6)
#define tgl_LED1_PWR_ON toggle_io(PORTC, 6)
#define read_LED1_PWR_ON read_io(PORTC, 6)

/* LED1_TRANSISTOR */
#define set_LED1_TRANSISTOR set_io(PORTD, 0)
#define clr_LED1_TRANSISTOR clear_io(PORTD, 0)
#define tgl_LED1_TRANSISTOR toggle_io(PORTD, 0)
#define read_LED1_TRANSISTOR read_io(PORTD, 0)

/* AUX1 */
#define set_AUX1 set_io(PORTD, 1)
#define clr_AUX1 clear_io(PORTD, 1)
#define tgl_AUX1 toggle_io(PORTD, 1)
#define read_AUX1 read_io(PORTD, 1)

/* AUX0 */
#define set_AUX0 set_io(PORTD, 2)
#define clr_AUX0 clear_io(PORTD, 2)
#define tgl_AUX0 toggle_io(PORTD, 2)
#define read_AUX0 read_io(PORTD, 2)

/* BOARD_LED0 */
#define set_BOARD_LED0 set_io(PORTC, 4)
#define clr_BOARD_LED0 clear_io(PORTC, 4)
#define tgl_BOARD_LED0 toggle_io(PORTC, 4)
#define read_BOARD_LED0 read_io(PORTC, 4)

/* OUT0 */
#define set_OUT0 set_io(PORTA, 2)
#define clr_OUT0 clear_io(PORTA, 2)
#define tgl_OUT0 toggle_io(PORTA, 2)
#define read_OUT0 read_io(PORTA, 2)

/* OUT1 */
#define set_OUT1 set_io(PORTD, 7)
#define clr_OUT1 clear_io(PORTD, 7)
#define tgl_OUT1 toggle_io(PORTD, 7)
#define read_OUT1 read_io(PORTD, 7)


/************************************************************************/
/* Registers' structure                                                 */
/************************************************************************/
typedef struct
{
	uint8_t REG_POWER_EN;
	uint8_t REG_LED_BEHAVING;
	uint8_t REG_LED_ON;
	uint8_t REG_IN_STATE;
	uint8_t REG_OUT_CONFIGURATION;
	uint8_t REG_IN_CONFIGURATION;
	uint8_t REG_LED_CONFIGURATION;
	uint8_t REG_LED0_SUPPLY_PWR_CONF;
	uint8_t REG_LED1_SUPPLY_PWR_CONF;
	float REG_LED0_PWM_FREQ;
	float REG_LED0_PWM_DCYCLE;
	uint16_t REG_LED0_PWM_PULSES;
	uint16_t REG_LED0_INTERVAL_ON;
	uint16_t REG_LED0_INTERVAL_OFF;
	uint16_t REG_LED0_INTERVAL_PULSES;
	uint16_t REG_LED0_INTERVAL_TAIL;
	uint16_t REG_LED0_INTERVAL_REPS;
	float REG_LED1_PWM_FREQ;
	float REG_LED1_PWM_DCYCLE;
	uint16_t REG_LED1_PWM_PULSES;
	uint16_t REG_LED1_INTERVAL_ON;
	uint16_t REG_LED1_INTERVAL_OFF;
	uint16_t REG_LED1_INTERVAL_PULSES;
	uint16_t REG_LED1_INTERVAL_TAIL;
	uint16_t REG_LED1_INTERVAL_REPS;
	float REG_LED0_PWM_FREQ_REAL;
	float REG_LED0_PWM_DCYCLE_REAL;
	float REG_LED1_PWM_FREQ_REAL;
	float REG_LED1_PWM_DCYCLE_REAL;
	uint8_t REG_AUX_DIG_OUT;
	uint8_t REG_AUX_SUPPLY_PWR_CONF;
	uint8_t REG_DUMMY0;
	uint8_t REG_DUMMY1;
	uint8_t REG_EVNT_ENABLE;
} AppRegs;

/************************************************************************/
/* Registers' address                                                   */
/************************************************************************/
/* Registers */
#define ADD_REG_POWER_EN                    32 // U8     Control the enable of both LEDs' supply
#define ADD_REG_LED_BEHAVING                33 // U8     Starts and stops the LED is behaving
#define ADD_REG_LED_ON                      34 // U8     Controls the on board's LED
#define ADD_REG_IN_STATE                    35 // U8     Reflects the state of both inputs
#define ADD_REG_OUT_CONFIGURATION           36 // U8     Configures which signal is connected to the digital outputs
#define ADD_REG_IN_CONFIGURATION            37 // U8     Configures the digital inputs
#define ADD_REG_LED_CONFIGURATION           38 // U8     Configures how LEDs will behave
#define ADD_REG_LED0_SUPPLY_PWR_CONF        39 // U8     Configuration of power to be aplied to LED0 [1;120]
#define ADD_REG_LED1_SUPPLY_PWR_CONF        40 // U8     Configuration of power to be aplied to LED1 [1;120]
#define ADD_REG_LED0_PWM_FREQ               41 // FLOAT  PWM frequency of LED0's power transistor [0.5;2000.0]
#define ADD_REG_LED0_PWM_DCYCLE             42 // FLOAT  PWM duty cycle of LED0's power transistor [0.1;99.9]
#define ADD_REG_LED0_PWM_PULSES             43 // U16    Number of PWM pulses (LED0) [1;65535]
#define ADD_REG_LED0_INTERVAL_ON            44 // U16    Time ON of LED0 (milliseconds) [1;65535]
#define ADD_REG_LED0_INTERVAL_OFF           45 // U16    Time OFF of LED0 (milliseconds) [1;65535]
#define ADD_REG_LED0_INTERVAL_PULSES        46 // U16    Number of pulses (LED0) [1;65535]
#define ADD_REG_LED0_INTERVAL_TAIL          47 // U16    Wait time between pulses (milliseconds) (LED0) [1;65535]
#define ADD_REG_LED0_INTERVAL_REPS          48 // U16    Number of repetitions of the entire scheme (LED0) [1;65535]
#define ADD_REG_LED1_PWM_FREQ               49 // FLOAT  PWM frequency of LED1's power transistor [0.5;2000.0]
#define ADD_REG_LED1_PWM_DCYCLE             50 // FLOAT  PWM duty cycle of LED1's power transistor [0.1;99.9]
#define ADD_REG_LED1_PWM_PULSES             51 // U16    Number of PWM pulses (LED1) [1;65535]
#define ADD_REG_LED1_INTERVAL_ON            52 // U16    Time ON of LED1 (milliseconds) [1;65535]
#define ADD_REG_LED1_INTERVAL_OFF           53 // U16    Time OFF of LED1 (milliseconds) [1;65535]
#define ADD_REG_LED1_INTERVAL_PULSES        54 // U16    Number of pulses (LED1) [1;65535]
#define ADD_REG_LED1_INTERVAL_TAIL          55 // U16    Wait time between pulses (milliseconds) (LED1) [1;65535]
#define ADD_REG_LED1_INTERVAL_REPS          56 // U16    Number of repetitions of the entire scheme (LED1) [1;65535]
#define ADD_REG_LED0_PWM_FREQ_REAL          57 // FLOAT  Real PWM frequency of LED0's power transistor
#define ADD_REG_LED0_PWM_DCYCLE_REAL        58 // FLOAT  Real PWM duty cycle of LED0's power transistor
#define ADD_REG_LED1_PWM_FREQ_REAL          59 // FLOAT  Real PWM frequency of LED1's power transistor
#define ADD_REG_LED1_PWM_DCYCLE_REAL        60 // FLOAT  Real PWM duty cycle of LED1's power transistor
#define ADD_REG_AUX_DIG_OUT                 61 // U8     Controls the auxiliar digital output
#define ADD_REG_AUX_SUPPLY_PWR_CONF         62 // U8     Configuration of power to be aplied to auxiliar LED [1;120]
#define ADD_REG_DUMMY0                      63 // U8     Not used
#define ADD_REG_DUMMY1                      64 // U8     Not used
#define ADD_REG_EVNT_ENABLE                 65 // U8     Enable the Events

/************************************************************************/
/* PWM Generator registers' memory limits                               */
/*                                                                      */
/* DON'T change the APP_REGS_ADD_MIN value !!!                          */
/* DON'T change these names !!!                                         */
/************************************************************************/
/* Memory limits */
#define APP_REGS_ADD_MIN                    0x20
#define APP_REGS_ADD_MAX                    0x41
#define APP_NBYTES_OF_REG_BANK              70

/************************************************************************/
/* Registers' bits                                                      */
/************************************************************************/
#define B_LED0_PWR_EN                      (1<<0)       // Enable LED0 supply when equal to 1
#define B_LED1_PWR_EN                      (1<<1)       // Enable LED1 supply when equal to 1
#define B_LED0_PWR_DIS                     (1<<2)       // Disable LED0 supply when equal to 1
#define B_LED1_PWR_DIS                     (1<<3)       // Disable LED1 supply when equal to 1
#define B_LED0_START                       (1<<0)       // Start configured behaviour on LED0 when equal to 1
#define B_LED1_START                       (1<<1)       // Start configured behaviour on LED1 when equal to 1
#define B_LED0_STOP                        (1<<2)       // Stop configured behaviour on LED0 when equal to 1
#define B_LED1_STOP                        (1<<3)       // Stop configured behaviour on LED1 when equal to 1
#define B_LED0_TO_ON                       (1<<0)       // Turn LED0 on if equal to 1
#define B_LED1_TO_ON                       (1<<1)       // Turn LED1 on if equal to 1
#define B_LED0_TO_OFF                      (1<<2)       // Turn LED0 off if equal to 1
#define B_LED1_TO_OFF                      (1<<3)       // Turn LED1 off if equal to 1
#define B_IN0                              (1<<0)       // State of input IN0
#define B_IN1                              (1<<1)       // State of input IN1
#define MSK_OUT0_CONF                      (7<<0)       // Select OUT0 function
#define GM_OUT0_LED0_PWR_EN                (0<<0)       // Equal to bit LED0_PWR_EN
#define GM_OUT0_LED1_PWR_EN                (1<<0)       // Equal to bit LED1_PWR_EN
#define GM_OUT0_LED0_START                 (2<<0)       // Equal to bit LED0_START
#define GM_OUT0_LED1_START                 (3<<0)       // Equal to bit LED1_START
#define GM_OUT0_LED0_ON                    (4<<0)       // Equal to bit LED0_ON
#define GM_OUT0_LED1_ON                    (5<<0)       // Equal to bit LED1_ON
#define GM_OUT0_IN0                        (6<<0)       // Equal to bit IN0
#define GM_OUT0_IN1                        (7<<0)       // Equal to bit IN1
#define MSK_OUT1_CONF                      (7<<4)       // Select OUT1 function
#define GM_OUT1_LED0_PWR_EN                (0<<0)       // Equal to bit LED0_PWR_EN
#define GM_OUT1_LED1_PWR_EN                (1<<0)       // Equal to bit LED1_PWR_EN
#define GM_OUT1_LED0_START                 (2<<0)       // Equal to bit LED0_START
#define GM_OUT1_LED1_START                 (3<<0)       // Equal to bit LED1_START
#define GM_OUT1_LED0_ON                    (4<<0)       // Equal to bit LED0_ON
#define GM_OUT1_LED1_ON                    (5<<0)       // Equal to bit LED1_ON
#define GM_OUT1_IN0                        (6<<0)       // Equal to bit IN0
#define GM_OUT1_IN1                        (7<<0)       // Equal to bit IN1
#define MSK_IN0_CONF                       (7<<0)       // Configure IN0
#define MSK_IN1_CONF                       (7<<4)       // Configure IN1
#define GM_IN0_CONF_LED0_PWR_EN            (0<<0)       // IN0 controls bit LED0_PWR_EN
#define GM_IN0_CONF_LED0_START             (1<<0)       // IN0 controls bit LED0_START
#define GM_IN0_CONF_LED0_ON                (2<<0)       // IN0 controls bit LED0_ON
#define GM_IN0_CONF_LED1_PWR_EN            (3<<0)       // IN0 controls bit LED1_PWR_EN
#define GM_IN0_CONF_LED1_START             (4<<0)       // IN0 controls bit LED1_START
#define GM_IN0_CONF_LED1_ON                (5<<0)       // IN0 controls bit LED1_ON
#define GM_IN0_CONF_NOT                    (6<<0)       // IN0 Controls nothing
#define GM_IN1_CONF_LED0_PWR_EN            (0<<4)       // IN1 controls bit LED0_PWR_EN
#define GM_IN1_CONF_LED0_START             (1<<4)       // IN1 controls bit LED0_START
#define GM_IN1_CONF_LED0_ON                (2<<4)       // IN1 controls bit LED0_ON
#define GM_IN1_CONF_LED1_PWR_EN            (3<<4)       // IN1 controls bit LED1_PWR_EN
#define GM_IN1_CONF_LED1_START             (4<<4)       // IN1 controls bit LED1_START
#define GM_IN1_CONF_LED1_ON                (5<<4)       // IN1 controls bit LED1_ON
#define GM_IN1_CONF_NOT                    (6<<4)       // IN1 Controls nothing
#define MSK_LED0_CONF                      (3<<0)       // Configure LED0
#define MSK_LED1_CONF                      (3<<4)       // Configure LED1
#define GM_LED0_PWM                        (0<<0)       // LED0 uses configured PWM
#define GM_LED0_INTERVAL                   (1<<0)       // LED0 uses configured intervals
#define GM_LED1_PWM                        (0<<4)       // LED1 uses configured PWM
#define GM_LED1_INTERVAL                   (1<<4)       // LED1 uses configured intervals
#define B_AUX0_TO_HIGH                     (1<<0)       // Turn AUX0 to high level if equal to 1
#define B_AUX1_TO_HIGH                     (1<<1)       // Turn AUX1 to high level if equal to 1
#define B_AUX0_TO_LOW                      (1<<2)       // Turn AUX0 to low level if equal to 1
#define B_AUX1_TO_LOW                      (1<<3)       // Turn AUX1 to low level if equal to 1
#define B_EVT_LED_ON                       (1<<0)       // Event of register LED_ON
#define B_EVT_IN_STATE                     (1<<1)       // Event of register IN_STATE

#endif /* _APP_REGS_H_ */