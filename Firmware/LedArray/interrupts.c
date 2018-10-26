#include "cpu.h"
#include "hwbp_core_types.h"
#include "app_ios_and_regs.h"
#include "app_funcs.h"
#include "hwbp_core.h"

/************************************************************************/
/* Declare application registers                                        */
/************************************************************************/
extern AppRegs app_regs;

/************************************************************************/
/* Interrupts from Timers                                               */
/************************************************************************/
// ISR(TCC0_OVF_vect, ISR_NAKED)
// ISR(TCD0_OVF_vect, ISR_NAKED)
// ISR(TCE0_OVF_vect, ISR_NAKED)
// ISR(TCF0_OVF_vect, ISR_NAKED)
// 
// ISR(TCC0_CCA_vect, ISR_NAKED)
// ISR(TCD0_CCA_vect, ISR_NAKED)
// ISR(TCE0_CCA_vect, ISR_NAKED)
// ISR(TCF0_CCA_vect, ISR_NAKED)
// 
// ISR(TCD1_OVF_vect, ISR_NAKED)
// 
// ISR(TCD1_CCA_vect, ISR_NAKED)

/************************************************************************/ 
/* IN0                                                                  */
/************************************************************************/
ISR(PORTD_INT0_vect, ISR_NAKED)
{
   uint8_t previous = app_regs.REG_IN_STATE;
   app_read_REG_IN_STATE();
   
   if (previous != app_regs.REG_IN_STATE)
      if (app_regs.REG_EVNT_ENABLE & B_EVT_IN_STATE)
         core_func_send_event(ADD_REG_IN_STATE, true);
   
	reti();
}

/************************************************************************/ 
/* IN1                                                                  */
/************************************************************************/
ISR(PORTD_INT1_vect, ISR_NAKED)
{
   uint8_t previous = app_regs.REG_IN_STATE;
   app_read_REG_IN_STATE();
   
   if (previous != app_regs.REG_IN_STATE)
      if (app_regs.REG_EVNT_ENABLE & B_EVT_IN_STATE)
      core_func_send_event(ADD_REG_IN_STATE, true);
   
   reti();
}

