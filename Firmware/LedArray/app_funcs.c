#include "app_funcs.h"
#include "app_ios_and_regs.h"
#include "hwbp_core.h"

#include "fly_pit_boxes.h"

/************************************************************************/
/* Create pointers to functions                                         */
/************************************************************************/
extern AppRegs app_regs;

void (*app_func_rd_pointer[])(void) = {
	&app_read_REG_POWER_EN,
	&app_read_REG_LED_BEHAVING,
	&app_read_REG_LED_ON,
	&app_read_REG_IN_STATE,
	&app_read_REG_OUT_CONFIGURATION,
	&app_read_REG_IN_CONFIGURATION,
	&app_read_REG_LED_CONFIGURATION,
	&app_read_REG_LED0_SUPPLY_PWR_CONF,
	&app_read_REG_LED1_SUPPLY_PWR_CONF,
	&app_read_REG_LED0_PWM_FREQ,
	&app_read_REG_LED0_PWM_DCYCLE,
	&app_read_REG_LED0_PWM_PULSES,
	&app_read_REG_LED0_INTERVAL_ON,
	&app_read_REG_LED0_INTERVAL_OFF,
	&app_read_REG_LED0_INTERVAL_PULSES,
	&app_read_REG_LED0_INTERVAL_TAIL,
	&app_read_REG_LED0_INTERVAL_REPS,
	&app_read_REG_LED1_PWM_FREQ,
	&app_read_REG_LED1_PWM_DCYCLE,
	&app_read_REG_LED1_PWM_PULSES,
	&app_read_REG_LED1_INTERVAL_ON,
	&app_read_REG_LED1_INTERVAL_OFF,
	&app_read_REG_LED1_INTERVAL_PULSES,
	&app_read_REG_LED1_INTERVAL_TAIL,
	&app_read_REG_LED1_INTERVAL_REPS,
	&app_read_REG_LED0_PWM_FREQ_REAL,
	&app_read_REG_LED0_PWM_DCYCLE_REAL,
	&app_read_REG_LED1_PWM_FREQ_REAL,
	&app_read_REG_LED1_PWM_DCYCLE_REAL,
	&app_read_REG_AUX_DIG_OUT,
	&app_read_REG_AUX_SUPPLY_PWR_CONF,
	&app_read_REG_OUT_STATE,
	&app_read_REG_DUMMY0,
	&app_read_REG_EVNT_ENABLE
};

bool (*app_func_wr_pointer[])(void*) = {
	&app_write_REG_POWER_EN,
	&app_write_REG_LED_BEHAVING,
	&app_write_REG_LED_ON,
	&app_write_REG_IN_STATE,
	&app_write_REG_OUT_CONFIGURATION,
	&app_write_REG_IN_CONFIGURATION,
	&app_write_REG_LED_CONFIGURATION,
	&app_write_REG_LED0_SUPPLY_PWR_CONF,
	&app_write_REG_LED1_SUPPLY_PWR_CONF,
	&app_write_REG_LED0_PWM_FREQ,
	&app_write_REG_LED0_PWM_DCYCLE,
	&app_write_REG_LED0_PWM_PULSES,
	&app_write_REG_LED0_INTERVAL_ON,
	&app_write_REG_LED0_INTERVAL_OFF,
	&app_write_REG_LED0_INTERVAL_PULSES,
	&app_write_REG_LED0_INTERVAL_TAIL,
	&app_write_REG_LED0_INTERVAL_REPS,
	&app_write_REG_LED1_PWM_FREQ,
	&app_write_REG_LED1_PWM_DCYCLE,
	&app_write_REG_LED1_PWM_PULSES,
	&app_write_REG_LED1_INTERVAL_ON,
	&app_write_REG_LED1_INTERVAL_OFF,
	&app_write_REG_LED1_INTERVAL_PULSES,
	&app_write_REG_LED1_INTERVAL_TAIL,
	&app_write_REG_LED1_INTERVAL_REPS,
	&app_write_REG_LED0_PWM_FREQ_REAL,
	&app_write_REG_LED0_PWM_DCYCLE_REAL,
	&app_write_REG_LED1_PWM_FREQ_REAL,
	&app_write_REG_LED1_PWM_DCYCLE_REAL,
	&app_write_REG_AUX_DIG_OUT,
	&app_write_REG_AUX_SUPPLY_PWR_CONF,
	&app_write_REG_OUT_STATE,
	&app_write_REG_DUMMY0,
	&app_write_REG_EVNT_ENABLE
};




#define UPDATE_BOARD_LED0 if (read_LED0_PWR_ON && read_LED0_TRANSISTOR) {if (core_bool_is_visual_enabled()) set_BOARD_LED0;} else {clr_BOARD_LED0;}
#define UPDATE_BOARD_LED1 if (read_LED1_PWR_ON && read_LED1_TRANSISTOR) {if (core_bool_is_visual_enabled()) set_BOARD_LED1;} else {clr_BOARD_LED1;}

/************************************************************************/
/* BEHAVIOURS                                                           */
/************************************************************************/
uint8_t led0_mode, led1_mode;
#define MODE_LED0_PWM 0
#define MODE_LED1_PWM 0
#define MODE_LED0_INTERVAL 1
#define MODE_LED1_INTERVAL 1

typedef struct
{
   uint16_t pulses;
} pwm_t;

typedef struct
{
   uint16_t on_ms, off_ms;
   uint16_t pulses;
   uint16_t tail_ms;
   uint16_t reps;
} interval_t;

typedef struct
{
   pwm_t pwm;
   interval_t interval;
} behaviour_t;

behaviour_t led0, led1;

void start_led0_pwm(void)
{
   uint8_t prescaler;
   uint16_t target_count, duty_cycle;
   
   if (calculate_timer_16bits(32000000, app_regs.REG_LED0_PWM_FREQ, &prescaler, &target_count))
   {
      clr_LED0_TRANSISTOR;
      
      if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT0_CONF) == GM_OUT0_LED0_ON)
         clr_OUT0;
      
      duty_cycle = app_regs.REG_LED0_PWM_DCYCLE/100.0 * target_count + 0.5;
      timer_type0_pwm(&TCC0, prescaler, target_count, duty_cycle, INT_LEVEL_LOW, INT_LEVEL_LOW);
      
      if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT0_CONF) == GM_OUT0_LED0_START)
         set_OUT0;

      app_regs.REG_LED_BEHAVING = (app_regs.REG_LED_BEHAVING & B_LED1_START) | B_LED0_START;
      app_regs.REG_LED_ON = (app_regs.REG_LED_ON & B_LED1_TO_ON) | B_LED0_TO_ON;
      
      UPDATE_BOARD_LED0;
      
      led0_mode = MODE_LED0_PWM;
      led0.pwm.pulses = app_regs.REG_LED0_PWM_PULSES;
   }
}

void start_led1_pwm(void)
{
   uint8_t prescaler;
   uint16_t target_count, duty_cycle;
   
   if (calculate_timer_16bits(32000000, app_regs.REG_LED1_PWM_FREQ, &prescaler, &target_count))
   {
      clr_LED1_TRANSISTOR;
      
      if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT1_CONF) == GM_OUT1_LED1_ON)
         clr_OUT1;
      
      duty_cycle = app_regs.REG_LED1_PWM_DCYCLE/100.0 * target_count + 0.5;
      timer_type0_pwm(&TCD0, prescaler, target_count, duty_cycle, INT_LEVEL_LOW, INT_LEVEL_LOW);
      
      if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT1_CONF) == GM_OUT1_LED1_START)
         set_OUT1;
      
      app_regs.REG_LED_BEHAVING = (app_regs.REG_LED_BEHAVING & B_LED0_START) | B_LED1_START;
      app_regs.REG_LED_ON = (app_regs.REG_LED_ON & B_LED0_TO_ON) | B_LED1_TO_ON;
      
      UPDATE_BOARD_LED1;
      
      led1_mode = MODE_LED1_PWM;
      led1.pwm.pulses = app_regs.REG_LED1_PWM_PULSES;
   }
}

void start_led0_interval(void)
{
   led0_mode = MODE_LED0_INTERVAL;
   led0.interval.on_ms = app_regs.REG_LED0_INTERVAL_ON;
   led0.interval.off_ms = app_regs.REG_LED0_INTERVAL_OFF;
   led0.interval.tail_ms = app_regs.REG_LED0_INTERVAL_TAIL;
   led0.interval.pulses = app_regs.REG_LED0_INTERVAL_PULSES;
   led0.interval.reps = app_regs.REG_LED0_INTERVAL_REPS;

   timer_type0_enable(&TCC0, TIMER_PRESCALER_DIV256, 125, INT_LEVEL_LOW); // 1ms
   
   set_LED0_TRANSISTOR;
   
   if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT0_CONF) == GM_OUT0_LED0_ON)
      set_OUT0;
   
   if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT0_CONF) == GM_OUT0_LED0_START)
      set_OUT0;
   
   app_regs.REG_LED_BEHAVING = (app_regs.REG_LED_BEHAVING & B_LED1_START) | B_LED0_START;
   app_regs.REG_LED_ON = (app_regs.REG_LED_ON & B_LED1_TO_ON) | B_LED0_TO_ON;

   UPDATE_BOARD_LED0;
}

void start_led1_interval(void)
{
   led1_mode = MODE_LED1_INTERVAL;
   led1.interval.on_ms = app_regs.REG_LED1_INTERVAL_ON;
   led1.interval.off_ms = app_regs.REG_LED1_INTERVAL_OFF;
   led1.interval.tail_ms = app_regs.REG_LED1_INTERVAL_TAIL;
   led1.interval.pulses = app_regs.REG_LED1_INTERVAL_PULSES;
   led1.interval.reps = app_regs.REG_LED1_INTERVAL_REPS;

   timer_type0_enable(&TCD0, TIMER_PRESCALER_DIV256, 125, INT_LEVEL_LOW); // 1ms   
   
   set_LED1_TRANSISTOR;
   
   if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT1_CONF) == GM_OUT1_LED1_ON)
      set_OUT1;
   
   if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT1_CONF) == GM_OUT1_LED1_START)
      set_OUT1;
   
   app_regs.REG_LED_BEHAVING = (app_regs.REG_LED_BEHAVING & B_LED0_START) | B_LED1_START;
   app_regs.REG_LED_ON = (app_regs.REG_LED_ON & B_LED0_TO_ON) | B_LED1_TO_ON;
      
   UPDATE_BOARD_LED1;
}

/* LED0 Duty Cycle */
ISR(TCC0_CCA_vect, ISR_NAKED)
{
   if (led0_mode == MODE_LED0_PWM)
   {
      if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT0_CONF) == GM_OUT0_LED0_ON)
         clr_OUT0;
      
      app_regs.REG_LED_ON = app_regs.REG_LED_ON & B_LED1_TO_ON;

      if (--led0.pwm.pulses == 0)
      {
         app_regs.REG_LED_BEHAVING = app_regs.REG_LED_BEHAVING & B_LED1_START;
         
         timer_type0_stop(&TCC0);
         
         if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT0_CONF) == GM_OUT0_LED0_START)
            clr_OUT0;
      }
   }
   
   UPDATE_BOARD_LED0;

   reti();
}

/* LED1 Duty Cycle */
ISR(TCD0_CCA_vect, ISR_NAKED)
{
   if (led1_mode == MODE_LED1_PWM)
   {
      if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT1_CONF) == GM_OUT1_LED1_ON)
         clr_OUT1;
         
      app_regs.REG_LED_ON = app_regs.REG_LED_ON & B_LED0_TO_ON;

      if (--led1.pwm.pulses == 0)
      {
         app_regs.REG_LED_BEHAVING = app_regs.REG_LED_BEHAVING & B_LED0_START;

         timer_type0_stop(&TCD0);
         
         if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT1_CONF) == GM_OUT1_LED1_START)
            clr_OUT1;
      }
   }
   
   UPDATE_BOARD_LED1;

   reti();
}


/* LED0 Overflow */
ISR(TCC0_OVF_vect, ISR_NAKED)
{
   if (led0_mode == MODE_LED0_PWM)
   {
      if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT0_CONF) == GM_OUT0_LED0_ON)
         set_OUT0;
         
      app_regs.REG_LED_ON = (app_regs.REG_LED_ON & B_LED1_TO_ON) | B_LED0_TO_ON;
      
      UPDATE_BOARD_LED0;
   }
   
   if (led0_mode == MODE_LED0_INTERVAL)
   {
      if (--led0.interval.on_ms == 0)
      {
         led0.interval.on_ms++;

         if (led0.interval.off_ms == app_regs.REG_LED0_INTERVAL_OFF)
         {
            clr_LED0_TRANSISTOR;
            
            if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT0_CONF) == GM_OUT0_LED0_ON)
               clr_OUT0;
               
            UPDATE_BOARD_LED0;
            
            app_regs.REG_LED_ON = app_regs.REG_LED_ON & B_LED1_TO_ON;
         }
         
         if (--led0.interval.off_ms + 1 == 0)
         {
            if (--led0.interval.pulses > 0)
            {
               set_LED0_TRANSISTOR;
               
               if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT0_CONF) == GM_OUT0_LED0_ON)
                  set_OUT0;
               
               UPDATE_BOARD_LED0;
               
               app_regs.REG_LED_ON = (app_regs.REG_LED_ON & B_LED1_TO_ON) | B_LED0_TO_ON;

               led0.interval.on_ms = app_regs.REG_LED0_INTERVAL_ON;
               led0.interval.off_ms = app_regs.REG_LED0_INTERVAL_OFF;
            }
            else
            {
               led0.interval.pulses++;
               led0.interval.off_ms++;

               if (--led0.interval.tail_ms + 1 == 0)
               {
                  if (--led0.interval.reps == 0)
                  {
                     app_regs.REG_LED_BEHAVING = app_regs.REG_LED_BEHAVING & B_LED1_START;
                     timer_type0_stop(&TCC0);
                     
                     if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT0_CONF) == GM_OUT0_LED0_START)
                        clr_OUT0;
                     
                     reti();
                  }

                  set_LED0_TRANSISTOR;
                  
                  if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT0_CONF) == GM_OUT0_LED0_ON)
                     set_OUT0;
               
                  UPDATE_BOARD_LED0;
                  
                  app_regs.REG_LED_ON = (app_regs.REG_LED_ON & B_LED1_TO_ON) | B_LED0_TO_ON;

                  led0.interval.on_ms = app_regs.REG_LED0_INTERVAL_ON;
                  led0.interval.off_ms = app_regs.REG_LED0_INTERVAL_OFF;
                  led0.interval.pulses = app_regs.REG_LED0_INTERVAL_PULSES;
                  led0.interval.tail_ms = app_regs.REG_LED0_INTERVAL_TAIL;
               }
            }
         }
      }
   }

   reti();
}

/* LED1 Overflow */
ISR(TCD0_OVF_vect, ISR_NAKED)
{
   if (led1_mode == MODE_LED1_PWM)
   {
      if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT1_CONF) == GM_OUT1_LED1_ON)
         set_OUT1;
         
      app_regs.REG_LED_ON = (app_regs.REG_LED_ON & B_LED0_TO_ON) | B_LED1_TO_ON;
      
      UPDATE_BOARD_LED1;
   }
   
   if (led1_mode == MODE_LED1_INTERVAL)
   {
      if (--led1.interval.on_ms == 0)
      {
         led1.interval.on_ms++;

         if (led1.interval.off_ms == app_regs.REG_LED1_INTERVAL_OFF)
         {
            clr_LED1_TRANSISTOR;
            
            if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT1_CONF) == GM_OUT1_LED1_ON)
               clr_OUT1;
               
            UPDATE_BOARD_LED1;
            
            app_regs.REG_LED_ON = app_regs.REG_LED_ON & B_LED0_TO_ON;
         }
         
         if (--led1.interval.off_ms + 1 == 0)
         {
            if (--led1.interval.pulses > 0)
            {
               set_LED1_TRANSISTOR;
            
               if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT1_CONF) == GM_OUT1_LED1_ON)
                  set_OUT1;
               
               UPDATE_BOARD_LED1;
               
               app_regs.REG_LED_ON = (app_regs.REG_LED_ON & B_LED0_TO_ON) | B_LED1_TO_ON;

               led1.interval.on_ms = app_regs.REG_LED1_INTERVAL_ON;
               led1.interval.off_ms = app_regs.REG_LED1_INTERVAL_OFF;
            }
            else
            {
               led1.interval.pulses++;
               led1.interval.off_ms++;

               if (--led1.interval.tail_ms + 1 == 0)
               {
                  if (--led1.interval.reps == 0)
                  {
                     app_regs.REG_LED_BEHAVING = app_regs.REG_LED_BEHAVING & B_LED0_START;
                     timer_type0_stop(&TCD0);
                     
                     if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT1_CONF) == GM_OUT1_LED1_START)
                        clr_OUT1;
                     
                     reti();
                  }

                  set_LED1_TRANSISTOR;
            
                  if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT1_CONF) == GM_OUT1_LED1_ON)
                     set_OUT1;
               
                  UPDATE_BOARD_LED1;
                  
                  app_regs.REG_LED_ON = (app_regs.REG_LED_ON & B_LED0_TO_ON) | B_LED1_TO_ON;

                  led1.interval.on_ms = app_regs.REG_LED1_INTERVAL_ON;
                  led1.interval.off_ms = app_regs.REG_LED1_INTERVAL_OFF;
                  led1.interval.pulses = app_regs.REG_LED1_INTERVAL_PULSES;
                  led1.interval.tail_ms = app_regs.REG_LED1_INTERVAL_TAIL;
               }
            }
         }
      }
   }

   reti();
}


/************************************************************************/
/* REG_POWER_EN                                                         */
/************************************************************************/
void app_read_REG_POWER_EN(void)
{
	app_regs.REG_POWER_EN  = (read_LED0_PWR_ON) ? B_LED0_PWR_EN : 0;
   app_regs.REG_POWER_EN |= (read_LED1_PWR_ON) ? B_LED1_PWR_EN : 0;
}

bool app_write_REG_POWER_EN(void *a)
{
	uint8_t reg = *((uint8_t*)a);

   if (reg & B_LED0_PWR_EN)
      set_LED0_PWR_ON;
   
   if (reg & B_LED0_PWR_DIS)
      clr_LED0_PWR_ON;

   if (reg & B_LED1_PWR_EN)
      set_LED1_PWR_ON;

   if (reg & B_LED1_PWR_DIS)
      clr_LED1_PWR_ON;
      
   if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT0_CONF) == GM_OUT0_LED0_PWR_EN)
      if (read_LED0_PWR_ON)
         set_OUT0; else clr_OUT0;
         
   if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT1_CONF) == GM_OUT1_LED1_PWR_EN)
      if (read_LED1_PWR_ON)
         set_OUT1; else clr_OUT1;
      
   UPDATE_BOARD_LED0;
   UPDATE_BOARD_LED1;

   return true;
}


/************************************************************************/
/* REG_LED_BEHAVING                                                     */
/************************************************************************/
void app_read_REG_LED_BEHAVING(void) {}
bool app_write_REG_LED_BEHAVING(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	if (reg & B_LED0_START)
   {
      if ((app_regs.REG_LED_CONFIGURATION & MSK_LED0_CONF) == GM_LED0_PWM)
      	start_led0_pwm();
      if ((app_regs.REG_LED_CONFIGURATION & MSK_LED0_CONF) == GM_LED0_INTERVAL)
      	start_led0_interval();
   }
   
   if (reg & B_LED0_STOP)
   {
      if (TCC0_CTRLA)
      {
         clr_LED0_TRANSISTOR;
      }

      app_regs.REG_LED_BEHAVING = app_regs.REG_LED_BEHAVING & ~(B_LED0_START);
      timer_type0_stop(&TCC0);
      
      if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT0_CONF) == GM_OUT0_LED0_START)
         clr_OUT0;
         
      if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT0_CONF) == GM_OUT0_LED0_ON)
         clr_OUT0;
      
      UPDATE_BOARD_LED0;
   }

   if (reg & B_LED1_START)
   {
      if ((app_regs.REG_LED_CONFIGURATION & MSK_LED1_CONF) == GM_LED1_PWM)
      	start_led1_pwm();
      if ((app_regs.REG_LED_CONFIGURATION & MSK_LED1_CONF) == GM_LED1_INTERVAL)
      	start_led1_interval();
   }
   
   if (reg & B_LED1_STOP)
   {
      if (TCD0_CTRLA)
      {
         clr_LED1_TRANSISTOR;
      }

      app_regs.REG_LED_BEHAVING = app_regs.REG_LED_BEHAVING & ~(B_LED1_START);
      timer_type0_stop(&TCD0);
      
      if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT1_CONF) == GM_OUT1_LED1_START)
         clr_OUT1;
         
      if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT1_CONF) == GM_OUT1_LED1_ON)
         clr_OUT1;
      
      UPDATE_BOARD_LED1;
	}

	return true;
}


/************************************************************************/
/* REG_LED_ON                                                           */
/************************************************************************/
void app_read_REG_LED_ON(void)
{
	app_regs.REG_LED_ON  = (read_LED0_TRANSISTOR) ? B_LED0_TO_ON : 0;
   app_regs.REG_LED_ON |= (read_LED1_TRANSISTOR) ? B_LED1_TO_ON : 0;
}

bool app_write_REG_LED_ON(void *a)
{
	uint8_t reg = *((uint8_t*)a);
   
   if (reg & B_LED0_TO_ON)
   {
      set_LED0_TRANSISTOR;
      UPDATE_BOARD_LED0;
   }      
   
   if (reg & B_LED0_TO_OFF)
   {
      clr_LED0_TRANSISTOR;
      UPDATE_BOARD_LED0;
   }
   
   if (reg & B_LED1_TO_ON)
   {
      set_LED1_TRANSISTOR;
      UPDATE_BOARD_LED1;
   }      
   
   if (reg & B_LED1_TO_OFF)
   {
      clr_LED1_TRANSISTOR;
      UPDATE_BOARD_LED1;
   }
   
   if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT0_CONF) == GM_OUT0_LED0_ON)
       if (read_LED0_TRANSISTOR)
           set_OUT0; else clr_OUT0;
           
   if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT1_CONF) == GM_OUT1_LED1_ON)
       if (read_LED1_TRANSISTOR)
           set_OUT1; else clr_OUT1;
   
	return true;
}


/************************************************************************/
/* REG_IN_STATE                                                         */
/************************************************************************/
void app_read_REG_IN_STATE(void)
{
	app_regs.REG_IN_STATE  = (read_IN0) ? B_IN0 : 0;
   app_regs.REG_IN_STATE |= (read_IN1) ? B_IN1 : 0;
}

bool app_write_REG_IN_STATE(void *a)
{
	return false;
}


/************************************************************************/
/* REG_OUT_CONFIGURATION                                                */
/************************************************************************/
void app_read_REG_OUT_CONFIGURATION(void) {}
bool app_write_REG_OUT_CONFIGURATION(void *a)
{   
    if (*((uint8_t*)a) & ~(MSK_OUT0_CONF | MSK_OUT1_CONF))
        return false;
    
    app_regs.REG_OUT_CONFIGURATION = *((uint8_t*)a);

   if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT0_CONF) == GM_OUT0_SOFTWARE)
       if (app_regs.REG_OUT_STATE & B_OUT0_TO_HIGH)
           set_OUT0; else clr_OUT0;           
   
   if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT1_CONF) == GM_OUT1_SOFTWARE)
       if (app_regs.REG_OUT_STATE & B_OUT1_TO_HIGH)
           set_OUT1; else clr_OUT1;
       
    if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT0_CONF) == GM_OUT0_LED0_PWR_EN)
        if (read_LED0_PWR_ON)
           set_OUT0; else clr_OUT0;
       
    if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT1_CONF) == GM_OUT1_LED1_PWR_EN)
        if (read_LED1_PWR_ON)
            set_OUT1; else clr_OUT1;
       
    if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT0_CONF) == GM_OUT0_LED0_START)
        if (app_regs.REG_LED_BEHAVING & B_LED0_START)
            set_OUT0; else clr_OUT0;
            
    if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT1_CONF) == GM_OUT1_LED1_START)
        if (app_regs.REG_LED_BEHAVING & B_LED1_START)
            set_OUT1; else clr_OUT1;
       
    if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT0_CONF) == GM_OUT0_LED0_ON)
       if (read_LED0_TRANSISTOR)
           set_OUT0; else clr_OUT0;
           
    if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT1_CONF) == GM_OUT1_LED1_ON)
        if (read_LED1_TRANSISTOR)
            set_OUT1; else clr_OUT1;
       
	return true;
}


/************************************************************************/
/* REG_IN_CONFIGURATION                                                 */
/************************************************************************/
void app_read_REG_IN_CONFIGURATION(void) {}
bool app_write_REG_IN_CONFIGURATION(void *a)
{
	app_regs.REG_IN_CONFIGURATION = *((uint8_t*)a);
	return true;
}


/************************************************************************/
/* REG_LED_CONFIGURATION                                                */
/************************************************************************/
void app_read_REG_LED_CONFIGURATION(void) {}
bool app_write_REG_LED_CONFIGURATION(void *a)
{
	uint8_t reg = *((uint8_t*)a);
   
   if (reg & ~(MSK_LED0_CONF | MSK_LED1_CONF))
      return false;

	app_regs.REG_LED_CONFIGURATION = reg;
	return true;
}


/************************************************************************/
/* REG_LED0_SUPPLY_PWR_CONF                                             */
/************************************************************************/
bool write_SMBus_word(uint8_t add, uint8_t reg, int16_t word);

void app_read_REG_LED0_SUPPLY_PWR_CONF(void) {}
bool app_write_REG_LED0_SUPPLY_PWR_CONF(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	if (reg < 1 || reg > 120)
		return false;

	if (!write_SMBus_word(17, 0x22, ((int16_t) reg) * 5 -300))
		core_func_catastrophic_error_detected();

	app_regs.REG_LED0_SUPPLY_PWR_CONF = reg;
	return true;
}


/************************************************************************/
/* REG_LED1_SUPPLY_PWR_CONF                                             */
/************************************************************************/
extern bool bus_expansion_exists;

void app_read_REG_LED1_SUPPLY_PWR_CONF(void) {}
bool app_write_REG_LED1_SUPPLY_PWR_CONF(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	if (reg < 1 || reg > 120)
		return false;	
   
	if (bus_expansion_exists == false)  // Check only if bus expansion exists in the bus
		write_SMBus_word(33, 0x22, ((int16_t) reg) * 5 -300);
		//   core_func_catastrophic_error_detected();

	app_regs.REG_LED1_SUPPLY_PWR_CONF = reg;
	return true;
}


/************************************************************************/
/* REG_LED0_PWM_FREQ                                                    */
/************************************************************************/
bool update_reals(float * real_freq, float * real_dcycle, float freq, float dcycle);

void app_read_REG_LED0_PWM_FREQ(void) {}
bool app_write_REG_LED0_PWM_FREQ(void *a)
{
	float reg = *((float*)a);
   
   /* Return false if timer is running */
	if (TCC0_CTRLA)
		return false;

	/* Check range */
	if (reg < 0.5 || reg > 2000.0)
		return false;

	float real_freq, real_dcycle;

	if (!update_reals(&real_freq, &real_dcycle, reg, app_regs.REG_LED0_PWM_DCYCLE))
		return false;

	app_regs.REG_LED0_PWM_FREQ_REAL = real_freq;
	app_regs.REG_LED0_PWM_DCYCLE_REAL = real_dcycle;

	app_regs.REG_LED0_PWM_FREQ = reg;
	return true;
}


/************************************************************************/
/* REG_LED0_PWM_DCYCLE                                                  */
/************************************************************************/
void app_read_REG_LED0_PWM_DCYCLE(void) {}
bool app_write_REG_LED0_PWM_DCYCLE(void *a)
{
	float reg = *((float*)a);
   
   /* Return false if timer is running */
	if (TCC0_CTRLA)
		return false;

	/* Check range */
	if (reg < 0.1 || reg > 99.9)
		return false;

	float real_freq, real_dcycle;

	if (!update_reals(&real_freq, &real_dcycle, app_regs.REG_LED0_PWM_FREQ, reg))
		return false;

	app_regs.REG_LED0_PWM_FREQ_REAL = real_freq;
	app_regs.REG_LED0_PWM_DCYCLE_REAL = real_dcycle;   

	app_regs.REG_LED0_PWM_DCYCLE = reg;
	return true;
}


/************************************************************************/
/* REG_LED0_PWM_PULSES                                                  */
/************************************************************************/
void app_read_REG_LED0_PWM_PULSES(void) {}
bool app_write_REG_LED0_PWM_PULSES(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	/* Return false if timer is running */
	if (TCC0_CTRLA)
		return false;

	/* Check range */
	if (reg < 1)
		return false;

	app_regs.REG_LED0_PWM_PULSES = reg;
	return true;
}


/************************************************************************/
/* REG_LED0_INTERVAL_ON                                                 */
/************************************************************************/
void app_read_REG_LED0_INTERVAL_ON(void) {}
bool app_write_REG_LED0_INTERVAL_ON(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	/* Return false if timer is running */
	if (TCC0_CTRLA)
		return false;

	/* Check range */
	if (reg < 1)
		return false;

	app_regs.REG_LED0_INTERVAL_ON = reg;
	return true;
}


/************************************************************************/
/* REG_LED0_INTERVAL_OFF                                                */
/************************************************************************/
void app_read_REG_LED0_INTERVAL_OFF(void) {}
bool app_write_REG_LED0_INTERVAL_OFF(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	/* Return false if timer is running */
	if (TCC0_CTRLA)
		return false;

	/* Check range */
	if (reg < 1)
		return false;

	app_regs.REG_LED0_INTERVAL_OFF = reg;
	return true;
}


/************************************************************************/
/* REG_LED0_INTERVAL_PULSES                                             */
/************************************************************************/
void app_read_REG_LED0_INTERVAL_PULSES(void) {}
bool app_write_REG_LED0_INTERVAL_PULSES(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	/* Return false if timer is running */
	if (TCC0_CTRLA)
		return false;

	/* Check range */
	if (reg < 1)
		return false;

	app_regs.REG_LED0_INTERVAL_PULSES = reg;
	return true;
}


/************************************************************************/
/* REG_LED0_INTERVAL_TAIL                                               */
/************************************************************************/
void app_read_REG_LED0_INTERVAL_TAIL(void) {}
bool app_write_REG_LED0_INTERVAL_TAIL(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	/* Return false if timer is running */
	if (TCC0_CTRLA)
		return false;

	/* Check range */
	if (reg < 1)
		return false;

	app_regs.REG_LED0_INTERVAL_TAIL = reg;
	return true;
}


/************************************************************************/
/* REG_LED0_INTERVAL_REPS                                               */
/************************************************************************/
void app_read_REG_LED0_INTERVAL_REPS(void) {}
bool app_write_REG_LED0_INTERVAL_REPS(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	/* Return false if timer is running */
	if (TCC0_CTRLA)
		return false;

	/* Check range */
	if (reg < 1)
		return false;

	app_regs.REG_LED0_INTERVAL_REPS = reg;
	return true;
}


/************************************************************************/
/* REG_LED1_PWM_FREQ                                                    */
/************************************************************************/
void app_read_REG_LED1_PWM_FREQ(void) {}
bool app_write_REG_LED1_PWM_FREQ(void *a)
{
	float reg = *((float*)a);

	/* Return false if timer is running */
	if (TCD0_CTRLA)
		return false;

	/* Check range */
	if (reg < 0.5 || reg > 2000.0)
		return false;

	float real_freq, real_dcycle;

	if (!update_reals(&real_freq, &real_dcycle, reg, app_regs.REG_LED1_PWM_DCYCLE))
		return false;

	app_regs.REG_LED1_PWM_FREQ_REAL = real_freq;
	app_regs.REG_LED1_PWM_DCYCLE_REAL = real_dcycle;

	app_regs.REG_LED1_PWM_FREQ = reg;
	return true;
}


/************************************************************************/
/* REG_LED1_PWM_DCYCLE                                                  */
/************************************************************************/
void app_read_REG_LED1_PWM_DCYCLE(void) {}
bool app_write_REG_LED1_PWM_DCYCLE(void *a)
{
	float reg = *((float*)a);
	
	/* Return false if timer is running */
	if (TCD0_CTRLA)
		return false;

	/* Check range */
	if (reg < 0.1 || reg > 99.9)
		return false;

	float real_freq, real_dcycle;

	if (!update_reals(&real_freq, &real_dcycle, app_regs.REG_LED1_PWM_FREQ, reg))
		return false;

	app_regs.REG_LED1_PWM_FREQ_REAL = real_freq;
	app_regs.REG_LED1_PWM_DCYCLE_REAL = real_dcycle;

	app_regs.REG_LED1_PWM_DCYCLE = reg;
	return true;
}


/************************************************************************/
/* REG_LED1_PWM_PULSES                                                  */
/************************************************************************/
void app_read_REG_LED1_PWM_PULSES(void) {}
bool app_write_REG_LED1_PWM_PULSES(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	/* Return false if timer is running */
	if (TCD0_CTRLA)
		return false;

	/* Check range */
	if (reg < 1)
		return false;

	app_regs.REG_LED1_PWM_PULSES = reg;
	return true;
}


/************************************************************************/
/* REG_LED1_INTERVAL_ON                                                 */
/************************************************************************/
void app_read_REG_LED1_INTERVAL_ON(void) {}
bool app_write_REG_LED1_INTERVAL_ON(void *a)
{
	uint16_t reg = *((uint16_t*)a);
	
	/* Return false if timer is running */
	if (TCD0_CTRLA)
		return false;

	/* Check range */
	if (reg < 1)
		return false;

	app_regs.REG_LED1_INTERVAL_ON = reg;
	return true;
}


/************************************************************************/
/* REG_LED1_INTERVAL_OFF                                                */
/************************************************************************/
void app_read_REG_LED1_INTERVAL_OFF(void) {}
bool app_write_REG_LED1_INTERVAL_OFF(void *a)
{
	uint16_t reg = *((uint16_t*)a);
		
	/* Return false if timer is running */
	if (TCD0_CTRLA)
		return false;

	/* Check range */
	if (reg < 1)
		return false;

	app_regs.REG_LED1_INTERVAL_OFF = reg;
	return true;
}


/************************************************************************/
/* REG_LED1_INTERVAL_PULSES                                             */
/************************************************************************/
void app_read_REG_LED1_INTERVAL_PULSES(void) {}
bool app_write_REG_LED1_INTERVAL_PULSES(void *a)
{
	uint16_t reg = *((uint16_t*)a);
		
	/* Return false if timer is running */
	if (TCD0_CTRLA)
		return false;

	/* Check range */
	if (reg < 1)
		return false;

	app_regs.REG_LED1_INTERVAL_PULSES = reg;
	return true;
}


/************************************************************************/
/* REG_LED1_INTERVAL_TAIL                                               */
/************************************************************************/
void app_read_REG_LED1_INTERVAL_TAIL(void) {}
bool app_write_REG_LED1_INTERVAL_TAIL(void *a)
{
	uint16_t reg = *((uint16_t*)a);
	
	/* Return false if timer is running */
	if (TCD0_CTRLA)
		return false;	

	/* Check range */
	if (reg < 1)
		return false;

	app_regs.REG_LED1_INTERVAL_TAIL = reg;
	return true;
}


/************************************************************************/
/* REG_LED1_INTERVAL_REPS                                               */
/************************************************************************/
void app_read_REG_LED1_INTERVAL_REPS(void) {}
bool app_write_REG_LED1_INTERVAL_REPS(void *a)
{
	uint16_t reg = *((uint16_t*)a);
	
	/* Return false if timer is running */
	if (TCD0_CTRLA)
		return false;

	/* Check range */
	if (reg < 1)
		return false;

	app_regs.REG_LED1_INTERVAL_REPS = reg;
	return true;
}


/************************************************************************/
/* REG_LED0_PWM_FREQ_REAL                                               */
/************************************************************************/
void app_read_REG_LED0_PWM_FREQ_REAL(void) {}
bool app_write_REG_LED0_PWM_FREQ_REAL(void *a)
{
   return false;
}


/************************************************************************/
/* REG_LED0_PWM_DCYCLE_REAL                                             */
/************************************************************************/
void app_read_REG_LED0_PWM_DCYCLE_REAL(void) {}
bool app_write_REG_LED0_PWM_DCYCLE_REAL(void *a)
{
   return false;
}


/************************************************************************/
/* REG_LED1_PWM_FREQ_REAL                                               */
/************************************************************************/
void app_read_REG_LED1_PWM_FREQ_REAL(void) {}
bool app_write_REG_LED1_PWM_FREQ_REAL(void *a)
{
   return false;
}


/************************************************************************/
/* REG_LED1_PWM_DCYCLE_REAL                                             */
/************************************************************************/
void app_read_REG_LED1_PWM_DCYCLE_REAL(void) {}
bool app_write_REG_LED1_PWM_DCYCLE_REAL(void *a)
{
   return false;
}


/************************************************************************/
/* REG_AUX_DIG_OUT                                                      */
/************************************************************************/
void app_read_REG_AUX_DIG_OUT(void)
{  
   app_regs.REG_AUX_DIG_OUT  = (read_AUX0) ? B_AUX0_TO_HIGH : 0;
   app_regs.REG_AUX_DIG_OUT |= (read_AUX1) ? B_AUX1_TO_HIGH : 0;
}
bool app_write_REG_AUX_DIG_OUT(void *a)
{
	uint8_t reg = *((uint8_t*)a);
   
   if (reg & B_AUX0_TO_HIGH)
      set_AUX0;
   
   if (reg & B_AUX0_TO_LOW)
      clr_AUX0;
   
   if (reg & B_AUX1_TO_HIGH)
      set_AUX1;
   
   if (reg & B_AUX1_TO_LOW)
      clr_AUX1;
      
	return true;
}


/************************************************************************/
/* REG_AUX_SUPPLY_PWR_CONF                                              */
/************************************************************************/
void app_read_REG_AUX_SUPPLY_PWR_CONF(void) {}
bool app_write_REG_AUX_SUPPLY_PWR_CONF(void *a)
{
	uint8_t reg = *((uint8_t*)a);
   
   if (reg < 1 || reg > 120)
		return false;	

	write_SMBus_word(25, 0x22, ((int16_t) reg) * 5 -300);

	app_regs.REG_AUX_SUPPLY_PWR_CONF = reg;
	return true;
}


/************************************************************************/
/* REG_DUMMY0                                                           */
/************************************************************************/
void app_read_REG_OUT_STATE(void) {}
bool app_write_REG_OUT_STATE(void *a)
{
	uint8_t reg = *((uint8_t*)a);
   
   if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT0_CONF) == GM_OUT0_SOFTWARE)
   {
      if (reg & B_OUT0_TO_HIGH)
         set_OUT0;
      
      if (reg & B_OUT0_TO_LOW)
         clr_OUT0;
   }
   
   if ((app_regs.REG_OUT_CONFIGURATION & MSK_OUT1_CONF) == GM_OUT1_SOFTWARE)
   {
      if (reg & B_OUT1_TO_HIGH)
         set_OUT1;
      
      if (reg & B_OUT1_TO_LOW)
         clr_OUT1;
   }
   
   app_regs.REG_OUT_STATE = reg & 7;
   
	return true;
}


/************************************************************************/
/* REG_DUMMY1                                                           */
/************************************************************************/
void app_read_REG_DUMMY0(void) {}
bool app_write_REG_DUMMY0(void *a)
{
	uint8_t reg = *((uint8_t*)a);
   
   update_leds_on_box(reg);
   
	app_regs.REG_DUMMY0 = reg;
	return true;
}


/************************************************************************/
/* REG_EVNT_ENABLE                                                      */
/************************************************************************/
void app_read_REG_EVNT_ENABLE(void)
{
	//app_regs.REG_EVNT_ENABLE = 0;

}

bool app_write_REG_EVNT_ENABLE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_EVNT_ENABLE = reg;
	return true;
}