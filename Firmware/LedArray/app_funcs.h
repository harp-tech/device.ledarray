#ifndef _APP_FUNCTIONS_H_
#define _APP_FUNCTIONS_H_
#include <avr/io.h>


/************************************************************************/
/* Define if not defined                                                */
/************************************************************************/
#ifndef bool
	#define bool uint8_t
#endif
#ifndef true
	#define true 1
#endif
#ifndef false
	#define false 0
#endif


/************************************************************************/
/* Prototypes                                                           */
/************************************************************************/
void app_read_REG_POWER_EN(void);
void app_read_REG_LED_BEHAVING(void);
void app_read_REG_LED_ON(void);
void app_read_REG_IN_STATE(void);
void app_read_REG_OUT_CONFIGURATION(void);
void app_read_REG_IN_CONFIGURATION(void);
void app_read_REG_LED_CONFIGURATION(void);
void app_read_REG_LED0_SUPPLY_PWR_CONF(void);
void app_read_REG_LED1_SUPPLY_PWR_CONF(void);
void app_read_REG_LED0_PWM_FREQ(void);
void app_read_REG_LED0_PWM_DCYCLE(void);
void app_read_REG_LED0_PWM_PULSES(void);
void app_read_REG_LED0_INTERVAL_ON(void);
void app_read_REG_LED0_INTERVAL_OFF(void);
void app_read_REG_LED0_INTERVAL_PULSES(void);
void app_read_REG_LED0_INTERVAL_TAIL(void);
void app_read_REG_LED0_INTERVAL_REPS(void);
void app_read_REG_LED1_PWM_FREQ(void);
void app_read_REG_LED1_PWM_DCYCLE(void);
void app_read_REG_LED1_PWM_PULSES(void);
void app_read_REG_LED1_INTERVAL_ON(void);
void app_read_REG_LED1_INTERVAL_OFF(void);
void app_read_REG_LED1_INTERVAL_PULSES(void);
void app_read_REG_LED1_INTERVAL_TAIL(void);
void app_read_REG_LED1_INTERVAL_REPS(void);
void app_read_REG_LED0_PWM_FREQ_REAL(void);
void app_read_REG_LED0_PWM_DCYCLE_REAL(void);
void app_read_REG_LED1_PWM_FREQ_REAL(void);
void app_read_REG_LED1_PWM_DCYCLE_REAL(void);
void app_read_REG_AUX_DIG_OUT(void);
void app_read_REG_AUX_SUPPLY_PWR_CONF(void);
void app_read_REG_OUT_STATE(void);
void app_read_REG_DUMMY0(void);
void app_read_REG_EVNT_ENABLE(void);

bool app_write_REG_POWER_EN(void *a);
bool app_write_REG_LED_BEHAVING(void *a);
bool app_write_REG_LED_ON(void *a);
bool app_write_REG_IN_STATE(void *a);
bool app_write_REG_OUT_CONFIGURATION(void *a);
bool app_write_REG_IN_CONFIGURATION(void *a);
bool app_write_REG_LED_CONFIGURATION(void *a);
bool app_write_REG_LED0_SUPPLY_PWR_CONF(void *a);
bool app_write_REG_LED1_SUPPLY_PWR_CONF(void *a);
bool app_write_REG_LED0_PWM_FREQ(void *a);
bool app_write_REG_LED0_PWM_DCYCLE(void *a);
bool app_write_REG_LED0_PWM_PULSES(void *a);
bool app_write_REG_LED0_INTERVAL_ON(void *a);
bool app_write_REG_LED0_INTERVAL_OFF(void *a);
bool app_write_REG_LED0_INTERVAL_PULSES(void *a);
bool app_write_REG_LED0_INTERVAL_TAIL(void *a);
bool app_write_REG_LED0_INTERVAL_REPS(void *a);
bool app_write_REG_LED1_PWM_FREQ(void *a);
bool app_write_REG_LED1_PWM_DCYCLE(void *a);
bool app_write_REG_LED1_PWM_PULSES(void *a);
bool app_write_REG_LED1_INTERVAL_ON(void *a);
bool app_write_REG_LED1_INTERVAL_OFF(void *a);
bool app_write_REG_LED1_INTERVAL_PULSES(void *a);
bool app_write_REG_LED1_INTERVAL_TAIL(void *a);
bool app_write_REG_LED1_INTERVAL_REPS(void *a);
bool app_write_REG_LED0_PWM_FREQ_REAL(void *a);
bool app_write_REG_LED0_PWM_DCYCLE_REAL(void *a);
bool app_write_REG_LED1_PWM_FREQ_REAL(void *a);
bool app_write_REG_LED1_PWM_DCYCLE_REAL(void *a);
bool app_write_REG_AUX_DIG_OUT(void *a);
bool app_write_REG_AUX_SUPPLY_PWR_CONF(void *a);
bool app_write_REG_OUT_STATE(void *a);
bool app_write_REG_DUMMY0(void *a);
bool app_write_REG_EVNT_ENABLE(void *a);


#endif /* _APP_FUNCTIONS_H_ */