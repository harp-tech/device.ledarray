using Bonsai;
using Bonsai.Harp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Xml.Serialization;

namespace Harp.LedArray
{
    /// <summary>
    /// Generates events and processes commands for the LedArray device connected
    /// at the specified serial port.
    /// </summary>
    [Combinator(MethodName = nameof(Generate))]
    [WorkflowElementCategory(ElementCategory.Source)]
    [Description("Generates events and processes commands for the LedArray device.")]
    public partial class Device : Bonsai.Harp.Device, INamedElement
    {
        /// <summary>
        /// Represents the unique identity class of the <see cref="LedArray"/> device.
        /// This field is constant.
        /// </summary>
        public const int WhoAmI = 1088;

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        public Device() : base(WhoAmI) { }

        string INamedElement.Name => nameof(LedArray);

        /// <summary>
        /// Gets a read-only mapping from address to register type.
        /// </summary>
        public static new IReadOnlyDictionary<int, Type> RegisterMap { get; } = new Dictionary<int, Type>
            (Bonsai.Harp.Device.RegisterMap.ToDictionary(entry => entry.Key, entry => entry.Value))
        {
            { 32, typeof(EnablePower) },
            { 33, typeof(EnableLedMode) },
            { 34, typeof(EnableLed) },
            { 35, typeof(DigitalInputState) },
            { 36, typeof(DigitalOutputSync) },
            { 37, typeof(DigitalInputTrigger) },
            { 38, typeof(PulseMode) },
            { 39, typeof(Led0Power) },
            { 40, typeof(Led1Power) },
            { 41, typeof(Led0PwmFrequency) },
            { 42, typeof(Led0PwmDutyCycle) },
            { 43, typeof(Led0PwmPulseCounter) },
            { 44, typeof(Led0PulseTimeOn) },
            { 45, typeof(Led0PulseTimeOff) },
            { 46, typeof(Led0PulseTimePulseCounter) },
            { 47, typeof(Led0PulseTimeTail) },
            { 48, typeof(Led0PulseRepeatCounter) },
            { 49, typeof(Led1PwmFrequency) },
            { 50, typeof(Led1PwmDutyCycle) },
            { 51, typeof(Led1PwmPulseCounter) },
            { 52, typeof(Led1PulseTimeOn) },
            { 53, typeof(Led1PulseTimeOff) },
            { 54, typeof(Led1PulseTimePulseCounter) },
            { 55, typeof(Led1PulseTimeTail) },
            { 56, typeof(Led1PulseRepeatCounter) },
            { 57, typeof(Led0PwmReal) },
            { 58, typeof(Led0PwmDutyCycleReal) },
            { 59, typeof(Led1PwmReal) },
            { 60, typeof(LedD1PwmDutyCycleReal) },
            { 61, typeof(AuxDigitalOutputState) },
            { 62, typeof(AuxLedPower) },
            { 63, typeof(DigitalOutputState) },
            { 65, typeof(EnableEvents) }
        };
    }

    /// <summary>
    /// Represents an operator that groups the sequence of <see cref="LedArray"/>" messages by register type.
    /// </summary>
    [Description("Groups the sequence of LedArray messages by register type.")]
    public partial class GroupByRegister : Combinator<HarpMessage, IGroupedObservable<Type, HarpMessage>>
    {
        /// <summary>
        /// Groups an observable sequence of <see cref="LedArray"/> messages
        /// by register type.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of observable groups, each of which corresponds to a unique
        /// <see cref="LedArray"/> register.
        /// </returns>
        public override IObservable<IGroupedObservable<Type, HarpMessage>> Process(IObservable<HarpMessage> source)
        {
            return source.GroupBy(message => Device.RegisterMap[message.Address]);
        }
    }

    /// <summary>
    /// Represents an operator that filters register-specific messages
    /// reported by the <see cref="LedArray"/> device.
    /// </summary>
    /// <seealso cref="EnablePower"/>
    /// <seealso cref="EnableLedMode"/>
    /// <seealso cref="EnableLed"/>
    /// <seealso cref="DigitalInputState"/>
    /// <seealso cref="DigitalOutputSync"/>
    /// <seealso cref="DigitalInputTrigger"/>
    /// <seealso cref="PulseMode"/>
    /// <seealso cref="Led0Power"/>
    /// <seealso cref="Led1Power"/>
    /// <seealso cref="Led0PwmFrequency"/>
    /// <seealso cref="Led0PwmDutyCycle"/>
    /// <seealso cref="Led0PwmPulseCounter"/>
    /// <seealso cref="Led0PulseTimeOn"/>
    /// <seealso cref="Led0PulseTimeOff"/>
    /// <seealso cref="Led0PulseTimePulseCounter"/>
    /// <seealso cref="Led0PulseTimeTail"/>
    /// <seealso cref="Led0PulseRepeatCounter"/>
    /// <seealso cref="Led1PwmFrequency"/>
    /// <seealso cref="Led1PwmDutyCycle"/>
    /// <seealso cref="Led1PwmPulseCounter"/>
    /// <seealso cref="Led1PulseTimeOn"/>
    /// <seealso cref="Led1PulseTimeOff"/>
    /// <seealso cref="Led1PulseTimePulseCounter"/>
    /// <seealso cref="Led1PulseTimeTail"/>
    /// <seealso cref="Led1PulseRepeatCounter"/>
    /// <seealso cref="Led0PwmReal"/>
    /// <seealso cref="Led0PwmDutyCycleReal"/>
    /// <seealso cref="Led1PwmReal"/>
    /// <seealso cref="LedD1PwmDutyCycleReal"/>
    /// <seealso cref="AuxDigitalOutputState"/>
    /// <seealso cref="AuxLedPower"/>
    /// <seealso cref="DigitalOutputState"/>
    /// <seealso cref="EnableEvents"/>
    [XmlInclude(typeof(EnablePower))]
    [XmlInclude(typeof(EnableLedMode))]
    [XmlInclude(typeof(EnableLed))]
    [XmlInclude(typeof(DigitalInputState))]
    [XmlInclude(typeof(DigitalOutputSync))]
    [XmlInclude(typeof(DigitalInputTrigger))]
    [XmlInclude(typeof(PulseMode))]
    [XmlInclude(typeof(Led0Power))]
    [XmlInclude(typeof(Led1Power))]
    [XmlInclude(typeof(Led0PwmFrequency))]
    [XmlInclude(typeof(Led0PwmDutyCycle))]
    [XmlInclude(typeof(Led0PwmPulseCounter))]
    [XmlInclude(typeof(Led0PulseTimeOn))]
    [XmlInclude(typeof(Led0PulseTimeOff))]
    [XmlInclude(typeof(Led0PulseTimePulseCounter))]
    [XmlInclude(typeof(Led0PulseTimeTail))]
    [XmlInclude(typeof(Led0PulseRepeatCounter))]
    [XmlInclude(typeof(Led1PwmFrequency))]
    [XmlInclude(typeof(Led1PwmDutyCycle))]
    [XmlInclude(typeof(Led1PwmPulseCounter))]
    [XmlInclude(typeof(Led1PulseTimeOn))]
    [XmlInclude(typeof(Led1PulseTimeOff))]
    [XmlInclude(typeof(Led1PulseTimePulseCounter))]
    [XmlInclude(typeof(Led1PulseTimeTail))]
    [XmlInclude(typeof(Led1PulseRepeatCounter))]
    [XmlInclude(typeof(Led0PwmReal))]
    [XmlInclude(typeof(Led0PwmDutyCycleReal))]
    [XmlInclude(typeof(Led1PwmReal))]
    [XmlInclude(typeof(LedD1PwmDutyCycleReal))]
    [XmlInclude(typeof(AuxDigitalOutputState))]
    [XmlInclude(typeof(AuxLedPower))]
    [XmlInclude(typeof(DigitalOutputState))]
    [XmlInclude(typeof(EnableEvents))]
    [Description("Filters register-specific messages reported by the LedArray device.")]
    public class FilterMessage : FilterMessageBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterMessage"/> class.
        /// </summary>
        public FilterMessage()
        {
            Register = new EnablePower();
        }

        string INamedElement.Name
        {
            get => $"{nameof(LedArray)}.{GetElementDisplayName(Register)}";
        }
    }

    /// <summary>
    /// Represents an operator which filters and selects specific messages
    /// reported by the LedArray device.
    /// </summary>
    /// <seealso cref="EnablePower"/>
    /// <seealso cref="EnableLedMode"/>
    /// <seealso cref="EnableLed"/>
    /// <seealso cref="DigitalInputState"/>
    /// <seealso cref="DigitalOutputSync"/>
    /// <seealso cref="DigitalInputTrigger"/>
    /// <seealso cref="PulseMode"/>
    /// <seealso cref="Led0Power"/>
    /// <seealso cref="Led1Power"/>
    /// <seealso cref="Led0PwmFrequency"/>
    /// <seealso cref="Led0PwmDutyCycle"/>
    /// <seealso cref="Led0PwmPulseCounter"/>
    /// <seealso cref="Led0PulseTimeOn"/>
    /// <seealso cref="Led0PulseTimeOff"/>
    /// <seealso cref="Led0PulseTimePulseCounter"/>
    /// <seealso cref="Led0PulseTimeTail"/>
    /// <seealso cref="Led0PulseRepeatCounter"/>
    /// <seealso cref="Led1PwmFrequency"/>
    /// <seealso cref="Led1PwmDutyCycle"/>
    /// <seealso cref="Led1PwmPulseCounter"/>
    /// <seealso cref="Led1PulseTimeOn"/>
    /// <seealso cref="Led1PulseTimeOff"/>
    /// <seealso cref="Led1PulseTimePulseCounter"/>
    /// <seealso cref="Led1PulseTimeTail"/>
    /// <seealso cref="Led1PulseRepeatCounter"/>
    /// <seealso cref="Led0PwmReal"/>
    /// <seealso cref="Led0PwmDutyCycleReal"/>
    /// <seealso cref="Led1PwmReal"/>
    /// <seealso cref="LedD1PwmDutyCycleReal"/>
    /// <seealso cref="AuxDigitalOutputState"/>
    /// <seealso cref="AuxLedPower"/>
    /// <seealso cref="DigitalOutputState"/>
    /// <seealso cref="EnableEvents"/>
    [XmlInclude(typeof(EnablePower))]
    [XmlInclude(typeof(EnableLedMode))]
    [XmlInclude(typeof(EnableLed))]
    [XmlInclude(typeof(DigitalInputState))]
    [XmlInclude(typeof(DigitalOutputSync))]
    [XmlInclude(typeof(DigitalInputTrigger))]
    [XmlInclude(typeof(PulseMode))]
    [XmlInclude(typeof(Led0Power))]
    [XmlInclude(typeof(Led1Power))]
    [XmlInclude(typeof(Led0PwmFrequency))]
    [XmlInclude(typeof(Led0PwmDutyCycle))]
    [XmlInclude(typeof(Led0PwmPulseCounter))]
    [XmlInclude(typeof(Led0PulseTimeOn))]
    [XmlInclude(typeof(Led0PulseTimeOff))]
    [XmlInclude(typeof(Led0PulseTimePulseCounter))]
    [XmlInclude(typeof(Led0PulseTimeTail))]
    [XmlInclude(typeof(Led0PulseRepeatCounter))]
    [XmlInclude(typeof(Led1PwmFrequency))]
    [XmlInclude(typeof(Led1PwmDutyCycle))]
    [XmlInclude(typeof(Led1PwmPulseCounter))]
    [XmlInclude(typeof(Led1PulseTimeOn))]
    [XmlInclude(typeof(Led1PulseTimeOff))]
    [XmlInclude(typeof(Led1PulseTimePulseCounter))]
    [XmlInclude(typeof(Led1PulseTimeTail))]
    [XmlInclude(typeof(Led1PulseRepeatCounter))]
    [XmlInclude(typeof(Led0PwmReal))]
    [XmlInclude(typeof(Led0PwmDutyCycleReal))]
    [XmlInclude(typeof(Led1PwmReal))]
    [XmlInclude(typeof(LedD1PwmDutyCycleReal))]
    [XmlInclude(typeof(AuxDigitalOutputState))]
    [XmlInclude(typeof(AuxLedPower))]
    [XmlInclude(typeof(DigitalOutputState))]
    [XmlInclude(typeof(EnableEvents))]
    [XmlInclude(typeof(TimestampedEnablePower))]
    [XmlInclude(typeof(TimestampedEnableLedMode))]
    [XmlInclude(typeof(TimestampedEnableLed))]
    [XmlInclude(typeof(TimestampedDigitalInputState))]
    [XmlInclude(typeof(TimestampedDigitalOutputSync))]
    [XmlInclude(typeof(TimestampedDigitalInputTrigger))]
    [XmlInclude(typeof(TimestampedPulseMode))]
    [XmlInclude(typeof(TimestampedLed0Power))]
    [XmlInclude(typeof(TimestampedLed1Power))]
    [XmlInclude(typeof(TimestampedLed0PwmFrequency))]
    [XmlInclude(typeof(TimestampedLed0PwmDutyCycle))]
    [XmlInclude(typeof(TimestampedLed0PwmPulseCounter))]
    [XmlInclude(typeof(TimestampedLed0PulseTimeOn))]
    [XmlInclude(typeof(TimestampedLed0PulseTimeOff))]
    [XmlInclude(typeof(TimestampedLed0PulseTimePulseCounter))]
    [XmlInclude(typeof(TimestampedLed0PulseTimeTail))]
    [XmlInclude(typeof(TimestampedLed0PulseRepeatCounter))]
    [XmlInclude(typeof(TimestampedLed1PwmFrequency))]
    [XmlInclude(typeof(TimestampedLed1PwmDutyCycle))]
    [XmlInclude(typeof(TimestampedLed1PwmPulseCounter))]
    [XmlInclude(typeof(TimestampedLed1PulseTimeOn))]
    [XmlInclude(typeof(TimestampedLed1PulseTimeOff))]
    [XmlInclude(typeof(TimestampedLed1PulseTimePulseCounter))]
    [XmlInclude(typeof(TimestampedLed1PulseTimeTail))]
    [XmlInclude(typeof(TimestampedLed1PulseRepeatCounter))]
    [XmlInclude(typeof(TimestampedLed0PwmReal))]
    [XmlInclude(typeof(TimestampedLed0PwmDutyCycleReal))]
    [XmlInclude(typeof(TimestampedLed1PwmReal))]
    [XmlInclude(typeof(TimestampedLedD1PwmDutyCycleReal))]
    [XmlInclude(typeof(TimestampedAuxDigitalOutputState))]
    [XmlInclude(typeof(TimestampedAuxLedPower))]
    [XmlInclude(typeof(TimestampedDigitalOutputState))]
    [XmlInclude(typeof(TimestampedEnableEvents))]
    [Description("Filters and selects specific messages reported by the LedArray device.")]
    public partial class Parse : ParseBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Parse"/> class.
        /// </summary>
        public Parse()
        {
            Register = new EnablePower();
        }

        string INamedElement.Name => $"{nameof(LedArray)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents an operator which formats a sequence of values as specific
    /// LedArray register messages.
    /// </summary>
    /// <seealso cref="EnablePower"/>
    /// <seealso cref="EnableLedMode"/>
    /// <seealso cref="EnableLed"/>
    /// <seealso cref="DigitalInputState"/>
    /// <seealso cref="DigitalOutputSync"/>
    /// <seealso cref="DigitalInputTrigger"/>
    /// <seealso cref="PulseMode"/>
    /// <seealso cref="Led0Power"/>
    /// <seealso cref="Led1Power"/>
    /// <seealso cref="Led0PwmFrequency"/>
    /// <seealso cref="Led0PwmDutyCycle"/>
    /// <seealso cref="Led0PwmPulseCounter"/>
    /// <seealso cref="Led0PulseTimeOn"/>
    /// <seealso cref="Led0PulseTimeOff"/>
    /// <seealso cref="Led0PulseTimePulseCounter"/>
    /// <seealso cref="Led0PulseTimeTail"/>
    /// <seealso cref="Led0PulseRepeatCounter"/>
    /// <seealso cref="Led1PwmFrequency"/>
    /// <seealso cref="Led1PwmDutyCycle"/>
    /// <seealso cref="Led1PwmPulseCounter"/>
    /// <seealso cref="Led1PulseTimeOn"/>
    /// <seealso cref="Led1PulseTimeOff"/>
    /// <seealso cref="Led1PulseTimePulseCounter"/>
    /// <seealso cref="Led1PulseTimeTail"/>
    /// <seealso cref="Led1PulseRepeatCounter"/>
    /// <seealso cref="Led0PwmReal"/>
    /// <seealso cref="Led0PwmDutyCycleReal"/>
    /// <seealso cref="Led1PwmReal"/>
    /// <seealso cref="LedD1PwmDutyCycleReal"/>
    /// <seealso cref="AuxDigitalOutputState"/>
    /// <seealso cref="AuxLedPower"/>
    /// <seealso cref="DigitalOutputState"/>
    /// <seealso cref="EnableEvents"/>
    [XmlInclude(typeof(EnablePower))]
    [XmlInclude(typeof(EnableLedMode))]
    [XmlInclude(typeof(EnableLed))]
    [XmlInclude(typeof(DigitalInputState))]
    [XmlInclude(typeof(DigitalOutputSync))]
    [XmlInclude(typeof(DigitalInputTrigger))]
    [XmlInclude(typeof(PulseMode))]
    [XmlInclude(typeof(Led0Power))]
    [XmlInclude(typeof(Led1Power))]
    [XmlInclude(typeof(Led0PwmFrequency))]
    [XmlInclude(typeof(Led0PwmDutyCycle))]
    [XmlInclude(typeof(Led0PwmPulseCounter))]
    [XmlInclude(typeof(Led0PulseTimeOn))]
    [XmlInclude(typeof(Led0PulseTimeOff))]
    [XmlInclude(typeof(Led0PulseTimePulseCounter))]
    [XmlInclude(typeof(Led0PulseTimeTail))]
    [XmlInclude(typeof(Led0PulseRepeatCounter))]
    [XmlInclude(typeof(Led1PwmFrequency))]
    [XmlInclude(typeof(Led1PwmDutyCycle))]
    [XmlInclude(typeof(Led1PwmPulseCounter))]
    [XmlInclude(typeof(Led1PulseTimeOn))]
    [XmlInclude(typeof(Led1PulseTimeOff))]
    [XmlInclude(typeof(Led1PulseTimePulseCounter))]
    [XmlInclude(typeof(Led1PulseTimeTail))]
    [XmlInclude(typeof(Led1PulseRepeatCounter))]
    [XmlInclude(typeof(Led0PwmReal))]
    [XmlInclude(typeof(Led0PwmDutyCycleReal))]
    [XmlInclude(typeof(Led1PwmReal))]
    [XmlInclude(typeof(LedD1PwmDutyCycleReal))]
    [XmlInclude(typeof(AuxDigitalOutputState))]
    [XmlInclude(typeof(AuxLedPower))]
    [XmlInclude(typeof(DigitalOutputState))]
    [XmlInclude(typeof(EnableEvents))]
    [Description("Formats a sequence of values as specific LedArray register messages.")]
    public partial class Format : FormatBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Format"/> class.
        /// </summary>
        public Format()
        {
            Register = new EnablePower();
        }

        string INamedElement.Name => $"{nameof(LedArray)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents a register that control the enable of both LEDs' power supply.
    /// </summary>
    [Description("Control the enable of both LEDs' power supply.")]
    public partial class EnablePower
    {
        /// <summary>
        /// Represents the address of the <see cref="EnablePower"/> register. This field is constant.
        /// </summary>
        public const int Address = 32;

        /// <summary>
        /// Represents the payload type of the <see cref="EnablePower"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EnablePower"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EnablePower"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static LedState GetPayload(HarpMessage message)
        {
            return (LedState)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EnablePower"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LedState> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((LedState)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EnablePower"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnablePower"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, LedState value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EnablePower"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnablePower"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, LedState value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EnablePower register.
    /// </summary>
    /// <seealso cref="EnablePower"/>
    [Description("Filters and selects timestamped messages from the EnablePower register.")]
    public partial class TimestampedEnablePower
    {
        /// <summary>
        /// Represents the address of the <see cref="EnablePower"/> register. This field is constant.
        /// </summary>
        public const int Address = EnablePower.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EnablePower"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LedState> GetPayload(HarpMessage message)
        {
            return EnablePower.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that start/stop the LEDs according to the pulse configuration.
    /// </summary>
    [Description("Start/stop the LEDs according to the pulse configuration.")]
    public partial class EnableLedMode
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableLedMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 33;

        /// <summary>
        /// Represents the payload type of the <see cref="EnableLedMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EnableLedMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EnableLedMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static LedState GetPayload(HarpMessage message)
        {
            return (LedState)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EnableLedMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LedState> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((LedState)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EnableLedMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableLedMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, LedState value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EnableLedMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableLedMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, LedState value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EnableLedMode register.
    /// </summary>
    /// <seealso cref="EnableLedMode"/>
    [Description("Filters and selects timestamped messages from the EnableLedMode register.")]
    public partial class TimestampedEnableLedMode
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableLedMode"/> register. This field is constant.
        /// </summary>
        public const int Address = EnableLedMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EnableLedMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LedState> GetPayload(HarpMessage message)
        {
            return EnableLedMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that enables/disables the LEDs.
    /// </summary>
    [Description("Enables/disables the LEDs.")]
    public partial class EnableLed
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableLed"/> register. This field is constant.
        /// </summary>
        public const int Address = 34;

        /// <summary>
        /// Represents the payload type of the <see cref="EnableLed"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EnableLed"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EnableLed"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static LedState GetPayload(HarpMessage message)
        {
            return (LedState)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EnableLed"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LedState> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((LedState)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EnableLed"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableLed"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, LedState value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EnableLed"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableLed"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, LedState value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EnableLed register.
    /// </summary>
    /// <seealso cref="EnableLed"/>
    [Description("Filters and selects timestamped messages from the EnableLed register.")]
    public partial class TimestampedEnableLed
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableLed"/> register. This field is constant.
        /// </summary>
        public const int Address = EnableLed.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EnableLed"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LedState> GetPayload(HarpMessage message)
        {
            return EnableLed.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that state of the digital input pins. An event will be emitted when the value of any digital input pin changes.
    /// </summary>
    [Description("State of the digital input pins. An event will be emitted when the value of any digital input pin changes.")]
    public partial class DigitalInputState
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInputState"/> register. This field is constant.
        /// </summary>
        public const int Address = 35;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalInputState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DigitalInputState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DigitalInputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalInputs GetPayload(HarpMessage message)
        {
            return (DigitalInputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalInputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalInputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalInputState"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInputState"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalInputState"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInputState"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalInputState register.
    /// </summary>
    /// <seealso cref="DigitalInputState"/>
    [Description("Filters and selects timestamped messages from the DigitalInputState register.")]
    public partial class TimestampedDigitalInputState
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInputState"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalInputState.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalInputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetPayload(HarpMessage message)
        {
            return DigitalInputState.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configuration of the digital outputs behavior.
    /// </summary>
    [Description("Configuration of the digital outputs behavior.")]
    public partial class DigitalOutputSync
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalOutputSync"/> register. This field is constant.
        /// </summary>
        public const int Address = 36;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalOutputSync"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DigitalOutputSync"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        static DigitalOutputSyncPayload ParsePayload(byte payload)
        {
            DigitalOutputSyncPayload result;
            result.DO0Sync = (DO0SyncConfig)(byte)(payload & 0x3);
            result.DO1Sync = (DO1SyncConfig)(byte)((payload & 0x30) >> 4);
            return result;
        }

        static byte FormatPayload(DigitalOutputSyncPayload value)
        {
            byte result;
            result = (byte)((byte)value.DO0Sync & 0x3);
            result |= (byte)(((byte)value.DO1Sync << 4) & 0x30);
            return result;
        }

        /// <summary>
        /// Returns the payload data for <see cref="DigitalOutputSync"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputSyncPayload GetPayload(HarpMessage message)
        {
            return ParsePayload(message.GetPayloadByte());
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalOutputSync"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputSyncPayload> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create(ParsePayload(payload.Value), payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalOutputSync"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalOutputSync"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputSyncPayload value)
        {
            return HarpMessage.FromByte(Address, messageType, FormatPayload(value));
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalOutputSync"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalOutputSync"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputSyncPayload value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, FormatPayload(value));
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalOutputSync register.
    /// </summary>
    /// <seealso cref="DigitalOutputSync"/>
    [Description("Filters and selects timestamped messages from the DigitalOutputSync register.")]
    public partial class TimestampedDigitalOutputSync
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalOutputSync"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalOutputSync.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalOutputSync"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputSyncPayload> GetPayload(HarpMessage message)
        {
            return DigitalOutputSync.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configuration of the digital inputs pins behavior.
    /// </summary>
    [Description("Configuration of the digital inputs pins behavior.")]
    public partial class DigitalInputTrigger
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInputTrigger"/> register. This field is constant.
        /// </summary>
        public const int Address = 37;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalInputTrigger"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DigitalInputTrigger"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        static DigitalInputTriggerPayload ParsePayload(byte payload)
        {
            DigitalInputTriggerPayload result;
            result.DI0Trigger = (DigitalInputTriggerConfig)(byte)(payload & 0x7);
            result.DI1Trigger = (DigitalInputTriggerConfig)(byte)((payload & 0x70) >> 4);
            return result;
        }

        static byte FormatPayload(DigitalInputTriggerPayload value)
        {
            byte result;
            result = (byte)((byte)value.DI0Trigger & 0x7);
            result |= (byte)(((byte)value.DI1Trigger << 4) & 0x70);
            return result;
        }

        /// <summary>
        /// Returns the payload data for <see cref="DigitalInputTrigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalInputTriggerPayload GetPayload(HarpMessage message)
        {
            return ParsePayload(message.GetPayloadByte());
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalInputTrigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputTriggerPayload> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create(ParsePayload(payload.Value), payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalInputTrigger"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInputTrigger"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalInputTriggerPayload value)
        {
            return HarpMessage.FromByte(Address, messageType, FormatPayload(value));
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalInputTrigger"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInputTrigger"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalInputTriggerPayload value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, FormatPayload(value));
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalInputTrigger register.
    /// </summary>
    /// <seealso cref="DigitalInputTrigger"/>
    [Description("Filters and selects timestamped messages from the DigitalInputTrigger register.")]
    public partial class TimestampedDigitalInputTrigger
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInputTrigger"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalInputTrigger.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalInputTrigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputTriggerPayload> GetPayload(HarpMessage message)
        {
            return DigitalInputTrigger.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the pulse mode used by the LEDs.
    /// </summary>
    [Description("Sets the pulse mode used by the LEDs.")]
    public partial class PulseMode
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 38;

        /// <summary>
        /// Represents the payload type of the <see cref="PulseMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="PulseMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        static PulseModePayload ParsePayload(byte payload)
        {
            PulseModePayload result;
            result.Led0Mode = (PulseModeConfig)(byte)(payload & 0x3);
            result.Led1Mode = (PulseModeConfig)(byte)((payload & 0x30) >> 4);
            return result;
        }

        static byte FormatPayload(PulseModePayload value)
        {
            byte result;
            result = (byte)((byte)value.Led0Mode & 0x3);
            result |= (byte)(((byte)value.Led1Mode << 4) & 0x30);
            return result;
        }

        /// <summary>
        /// Returns the payload data for <see cref="PulseMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static PulseModePayload GetPayload(HarpMessage message)
        {
            return ParsePayload(message.GetPayloadByte());
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PulseMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<PulseModePayload> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create(ParsePayload(payload.Value), payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PulseMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, PulseModePayload value)
        {
            return HarpMessage.FromByte(Address, messageType, FormatPayload(value));
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PulseMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, PulseModePayload value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, FormatPayload(value));
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PulseMode register.
    /// </summary>
    /// <seealso cref="PulseMode"/>
    [Description("Filters and selects timestamped messages from the PulseMode register.")]
    public partial class TimestampedPulseMode
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseMode"/> register. This field is constant.
        /// </summary>
        public const int Address = PulseMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PulseMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<PulseModePayload> GetPayload(HarpMessage message)
        {
            return PulseMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the power to LED0, between 1 and 120 (arbitrary units).
    /// </summary>
    [Description("Sets the power to LED0, between 1 and 120 (arbitrary units).")]
    public partial class Led0Power
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0Power"/> register. This field is constant.
        /// </summary>
        public const int Address = 39;

        /// <summary>
        /// Represents the payload type of the <see cref="Led0Power"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Led0Power"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led0Power"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led0Power"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led0Power"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0Power"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led0Power"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0Power"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led0Power register.
    /// </summary>
    /// <seealso cref="Led0Power"/>
    [Description("Filters and selects timestamped messages from the Led0Power register.")]
    public partial class TimestampedLed0Power
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0Power"/> register. This field is constant.
        /// </summary>
        public const int Address = Led0Power.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led0Power"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return Led0Power.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the power to LED1, between 1 and 120 (arbitrary units).
    /// </summary>
    [Description("Sets the power to LED1, between 1 and 120 (arbitrary units).")]
    public partial class Led1Power
    {
        /// <summary>
        /// Represents the address of the <see cref="Led1Power"/> register. This field is constant.
        /// </summary>
        public const int Address = 40;

        /// <summary>
        /// Represents the payload type of the <see cref="Led1Power"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Led1Power"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led1Power"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led1Power"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led1Power"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led1Power"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led1Power"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led1Power"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led1Power register.
    /// </summary>
    /// <seealso cref="Led1Power"/>
    [Description("Filters and selects timestamped messages from the Led1Power register.")]
    public partial class TimestampedLed1Power
    {
        /// <summary>
        /// Represents the address of the <see cref="Led1Power"/> register. This field is constant.
        /// </summary>
        public const int Address = Led1Power.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led1Power"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return Led1Power.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the frequency (Hz) of LED0 when in Pwm mode, between 0.5 and 2000.
    /// </summary>
    [Description("Sets the frequency (Hz) of LED0 when in Pwm mode, between 0.5 and 2000.")]
    public partial class Led0PwmFrequency
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0PwmFrequency"/> register. This field is constant.
        /// </summary>
        public const int Address = 41;

        /// <summary>
        /// Represents the payload type of the <see cref="Led0PwmFrequency"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Led0PwmFrequency"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led0PwmFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led0PwmFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led0PwmFrequency"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0PwmFrequency"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led0PwmFrequency"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0PwmFrequency"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led0PwmFrequency register.
    /// </summary>
    /// <seealso cref="Led0PwmFrequency"/>
    [Description("Filters and selects timestamped messages from the Led0PwmFrequency register.")]
    public partial class TimestampedLed0PwmFrequency
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0PwmFrequency"/> register. This field is constant.
        /// </summary>
        public const int Address = Led0PwmFrequency.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led0PwmFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Led0PwmFrequency.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the duty cycle (%) of LED0 when in Pwm mode, between 0.1 and 99.9.
    /// </summary>
    [Description("Sets the duty cycle (%) of LED0 when in Pwm mode, between 0.1 and 99.9.")]
    public partial class Led0PwmDutyCycle
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0PwmDutyCycle"/> register. This field is constant.
        /// </summary>
        public const int Address = 42;

        /// <summary>
        /// Represents the payload type of the <see cref="Led0PwmDutyCycle"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Led0PwmDutyCycle"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led0PwmDutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led0PwmDutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led0PwmDutyCycle"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0PwmDutyCycle"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led0PwmDutyCycle"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0PwmDutyCycle"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led0PwmDutyCycle register.
    /// </summary>
    /// <seealso cref="Led0PwmDutyCycle"/>
    [Description("Filters and selects timestamped messages from the Led0PwmDutyCycle register.")]
    public partial class TimestampedLed0PwmDutyCycle
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0PwmDutyCycle"/> register. This field is constant.
        /// </summary>
        public const int Address = Led0PwmDutyCycle.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led0PwmDutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Led0PwmDutyCycle.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the number of pulses of LED0 when in Pwm mode, between 1 and 65535.
    /// </summary>
    [Description("Sets the number of pulses of LED0 when in Pwm mode, between 1 and 65535.")]
    public partial class Led0PwmPulseCounter
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0PwmPulseCounter"/> register. This field is constant.
        /// </summary>
        public const int Address = 43;

        /// <summary>
        /// Represents the payload type of the <see cref="Led0PwmPulseCounter"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Led0PwmPulseCounter"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led0PwmPulseCounter"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led0PwmPulseCounter"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led0PwmPulseCounter"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0PwmPulseCounter"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led0PwmPulseCounter"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0PwmPulseCounter"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led0PwmPulseCounter register.
    /// </summary>
    /// <seealso cref="Led0PwmPulseCounter"/>
    [Description("Filters and selects timestamped messages from the Led0PwmPulseCounter register.")]
    public partial class TimestampedLed0PwmPulseCounter
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0PwmPulseCounter"/> register. This field is constant.
        /// </summary>
        public const int Address = Led0PwmPulseCounter.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led0PwmPulseCounter"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Led0PwmPulseCounter.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the time on (milliseconds) of LED0 when in PulseTime mode, between 1 and 65535.
    /// </summary>
    [Description("Sets the time on (milliseconds) of LED0 when in PulseTime mode, between 1 and 65535.")]
    public partial class Led0PulseTimeOn
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0PulseTimeOn"/> register. This field is constant.
        /// </summary>
        public const int Address = 44;

        /// <summary>
        /// Represents the payload type of the <see cref="Led0PulseTimeOn"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Led0PulseTimeOn"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led0PulseTimeOn"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led0PulseTimeOn"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led0PulseTimeOn"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0PulseTimeOn"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led0PulseTimeOn"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0PulseTimeOn"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led0PulseTimeOn register.
    /// </summary>
    /// <seealso cref="Led0PulseTimeOn"/>
    [Description("Filters and selects timestamped messages from the Led0PulseTimeOn register.")]
    public partial class TimestampedLed0PulseTimeOn
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0PulseTimeOn"/> register. This field is constant.
        /// </summary>
        public const int Address = Led0PulseTimeOn.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led0PulseTimeOn"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Led0PulseTimeOn.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the time off (milliseconds) of LED0 when in PulseTime mode, between 1 and 65535.
    /// </summary>
    [Description("Sets the time off (milliseconds) of LED0 when in PulseTime mode, between 1 and 65535.")]
    public partial class Led0PulseTimeOff
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0PulseTimeOff"/> register. This field is constant.
        /// </summary>
        public const int Address = 45;

        /// <summary>
        /// Represents the payload type of the <see cref="Led0PulseTimeOff"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Led0PulseTimeOff"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led0PulseTimeOff"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led0PulseTimeOff"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led0PulseTimeOff"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0PulseTimeOff"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led0PulseTimeOff"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0PulseTimeOff"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led0PulseTimeOff register.
    /// </summary>
    /// <seealso cref="Led0PulseTimeOff"/>
    [Description("Filters and selects timestamped messages from the Led0PulseTimeOff register.")]
    public partial class TimestampedLed0PulseTimeOff
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0PulseTimeOff"/> register. This field is constant.
        /// </summary>
        public const int Address = Led0PulseTimeOff.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led0PulseTimeOff"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Led0PulseTimeOff.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the number of pulses of LED0 when in PulseTime mode, between 1 and 65535.
    /// </summary>
    [Description("Sets the number of pulses of LED0 when in PulseTime mode, between 1 and 65535.")]
    public partial class Led0PulseTimePulseCounter
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0PulseTimePulseCounter"/> register. This field is constant.
        /// </summary>
        public const int Address = 46;

        /// <summary>
        /// Represents the payload type of the <see cref="Led0PulseTimePulseCounter"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Led0PulseTimePulseCounter"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led0PulseTimePulseCounter"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led0PulseTimePulseCounter"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led0PulseTimePulseCounter"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0PulseTimePulseCounter"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led0PulseTimePulseCounter"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0PulseTimePulseCounter"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led0PulseTimePulseCounter register.
    /// </summary>
    /// <seealso cref="Led0PulseTimePulseCounter"/>
    [Description("Filters and selects timestamped messages from the Led0PulseTimePulseCounter register.")]
    public partial class TimestampedLed0PulseTimePulseCounter
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0PulseTimePulseCounter"/> register. This field is constant.
        /// </summary>
        public const int Address = Led0PulseTimePulseCounter.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led0PulseTimePulseCounter"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Led0PulseTimePulseCounter.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the wait time between pulses (milliseconds) of LED0 when in PulseTime mode, between 1 and 65535.
    /// </summary>
    [Description("Sets the wait time between pulses (milliseconds) of LED0 when in PulseTime mode, between 1 and 65535.")]
    public partial class Led0PulseTimeTail
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0PulseTimeTail"/> register. This field is constant.
        /// </summary>
        public const int Address = 47;

        /// <summary>
        /// Represents the payload type of the <see cref="Led0PulseTimeTail"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Led0PulseTimeTail"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led0PulseTimeTail"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led0PulseTimeTail"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led0PulseTimeTail"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0PulseTimeTail"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led0PulseTimeTail"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0PulseTimeTail"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led0PulseTimeTail register.
    /// </summary>
    /// <seealso cref="Led0PulseTimeTail"/>
    [Description("Filters and selects timestamped messages from the Led0PulseTimeTail register.")]
    public partial class TimestampedLed0PulseTimeTail
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0PulseTimeTail"/> register. This field is constant.
        /// </summary>
        public const int Address = Led0PulseTimeTail.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led0PulseTimeTail"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Led0PulseTimeTail.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the number of repetitions of LED0 pulse protocol when in PulseTime mode, between 1 and 65535.
    /// </summary>
    [Description("Sets the number of repetitions of LED0 pulse protocol when in PulseTime mode, between 1 and 65535.")]
    public partial class Led0PulseRepeatCounter
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0PulseRepeatCounter"/> register. This field is constant.
        /// </summary>
        public const int Address = 48;

        /// <summary>
        /// Represents the payload type of the <see cref="Led0PulseRepeatCounter"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Led0PulseRepeatCounter"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led0PulseRepeatCounter"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led0PulseRepeatCounter"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led0PulseRepeatCounter"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0PulseRepeatCounter"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led0PulseRepeatCounter"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0PulseRepeatCounter"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led0PulseRepeatCounter register.
    /// </summary>
    /// <seealso cref="Led0PulseRepeatCounter"/>
    [Description("Filters and selects timestamped messages from the Led0PulseRepeatCounter register.")]
    public partial class TimestampedLed0PulseRepeatCounter
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0PulseRepeatCounter"/> register. This field is constant.
        /// </summary>
        public const int Address = Led0PulseRepeatCounter.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led0PulseRepeatCounter"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Led0PulseRepeatCounter.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the frequency (Hz) of LED1 when in Pwm mode, between 0.5 and 2000.
    /// </summary>
    [Description("Sets the frequency (Hz) of LED1 when in Pwm mode, between 0.5 and 2000.")]
    public partial class Led1PwmFrequency
    {
        /// <summary>
        /// Represents the address of the <see cref="Led1PwmFrequency"/> register. This field is constant.
        /// </summary>
        public const int Address = 49;

        /// <summary>
        /// Represents the payload type of the <see cref="Led1PwmFrequency"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Led1PwmFrequency"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led1PwmFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led1PwmFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led1PwmFrequency"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led1PwmFrequency"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led1PwmFrequency"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led1PwmFrequency"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led1PwmFrequency register.
    /// </summary>
    /// <seealso cref="Led1PwmFrequency"/>
    [Description("Filters and selects timestamped messages from the Led1PwmFrequency register.")]
    public partial class TimestampedLed1PwmFrequency
    {
        /// <summary>
        /// Represents the address of the <see cref="Led1PwmFrequency"/> register. This field is constant.
        /// </summary>
        public const int Address = Led1PwmFrequency.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led1PwmFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Led1PwmFrequency.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the duty cycle (%) of LED1 when in Pwm mode, between 0.1 and 99.9.
    /// </summary>
    [Description("Sets the duty cycle (%) of LED1 when in Pwm mode, between 0.1 and 99.9.")]
    public partial class Led1PwmDutyCycle
    {
        /// <summary>
        /// Represents the address of the <see cref="Led1PwmDutyCycle"/> register. This field is constant.
        /// </summary>
        public const int Address = 50;

        /// <summary>
        /// Represents the payload type of the <see cref="Led1PwmDutyCycle"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Led1PwmDutyCycle"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led1PwmDutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led1PwmDutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led1PwmDutyCycle"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led1PwmDutyCycle"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led1PwmDutyCycle"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led1PwmDutyCycle"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led1PwmDutyCycle register.
    /// </summary>
    /// <seealso cref="Led1PwmDutyCycle"/>
    [Description("Filters and selects timestamped messages from the Led1PwmDutyCycle register.")]
    public partial class TimestampedLed1PwmDutyCycle
    {
        /// <summary>
        /// Represents the address of the <see cref="Led1PwmDutyCycle"/> register. This field is constant.
        /// </summary>
        public const int Address = Led1PwmDutyCycle.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led1PwmDutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Led1PwmDutyCycle.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the number of pulses of LED1 when in Pwm mode, between 1 and 65535.
    /// </summary>
    [Description("Sets the number of pulses of LED1 when in Pwm mode, between 1 and 65535.")]
    public partial class Led1PwmPulseCounter
    {
        /// <summary>
        /// Represents the address of the <see cref="Led1PwmPulseCounter"/> register. This field is constant.
        /// </summary>
        public const int Address = 51;

        /// <summary>
        /// Represents the payload type of the <see cref="Led1PwmPulseCounter"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Led1PwmPulseCounter"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led1PwmPulseCounter"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led1PwmPulseCounter"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led1PwmPulseCounter"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led1PwmPulseCounter"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led1PwmPulseCounter"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led1PwmPulseCounter"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led1PwmPulseCounter register.
    /// </summary>
    /// <seealso cref="Led1PwmPulseCounter"/>
    [Description("Filters and selects timestamped messages from the Led1PwmPulseCounter register.")]
    public partial class TimestampedLed1PwmPulseCounter
    {
        /// <summary>
        /// Represents the address of the <see cref="Led1PwmPulseCounter"/> register. This field is constant.
        /// </summary>
        public const int Address = Led1PwmPulseCounter.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led1PwmPulseCounter"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Led1PwmPulseCounter.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the time on (milliseconds) of LED1 when in PulseTime mode, between 1 and 65535.
    /// </summary>
    [Description("Sets the time on (milliseconds) of LED1 when in PulseTime mode, between 1 and 65535.")]
    public partial class Led1PulseTimeOn
    {
        /// <summary>
        /// Represents the address of the <see cref="Led1PulseTimeOn"/> register. This field is constant.
        /// </summary>
        public const int Address = 52;

        /// <summary>
        /// Represents the payload type of the <see cref="Led1PulseTimeOn"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Led1PulseTimeOn"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led1PulseTimeOn"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led1PulseTimeOn"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led1PulseTimeOn"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led1PulseTimeOn"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led1PulseTimeOn"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led1PulseTimeOn"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led1PulseTimeOn register.
    /// </summary>
    /// <seealso cref="Led1PulseTimeOn"/>
    [Description("Filters and selects timestamped messages from the Led1PulseTimeOn register.")]
    public partial class TimestampedLed1PulseTimeOn
    {
        /// <summary>
        /// Represents the address of the <see cref="Led1PulseTimeOn"/> register. This field is constant.
        /// </summary>
        public const int Address = Led1PulseTimeOn.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led1PulseTimeOn"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Led1PulseTimeOn.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the time off (milliseconds) of LED1 when in PulseTime mode, between 1 and 65535.
    /// </summary>
    [Description("Sets the time off (milliseconds) of LED1 when in PulseTime mode, between 1 and 65535.")]
    public partial class Led1PulseTimeOff
    {
        /// <summary>
        /// Represents the address of the <see cref="Led1PulseTimeOff"/> register. This field is constant.
        /// </summary>
        public const int Address = 53;

        /// <summary>
        /// Represents the payload type of the <see cref="Led1PulseTimeOff"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Led1PulseTimeOff"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led1PulseTimeOff"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led1PulseTimeOff"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led1PulseTimeOff"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led1PulseTimeOff"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led1PulseTimeOff"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led1PulseTimeOff"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led1PulseTimeOff register.
    /// </summary>
    /// <seealso cref="Led1PulseTimeOff"/>
    [Description("Filters and selects timestamped messages from the Led1PulseTimeOff register.")]
    public partial class TimestampedLed1PulseTimeOff
    {
        /// <summary>
        /// Represents the address of the <see cref="Led1PulseTimeOff"/> register. This field is constant.
        /// </summary>
        public const int Address = Led1PulseTimeOff.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led1PulseTimeOff"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Led1PulseTimeOff.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the number of pulses of LED1 when in PulseTime mode, between 1 and 65535.
    /// </summary>
    [Description("Sets the number of pulses of LED1 when in PulseTime mode, between 1 and 65535.")]
    public partial class Led1PulseTimePulseCounter
    {
        /// <summary>
        /// Represents the address of the <see cref="Led1PulseTimePulseCounter"/> register. This field is constant.
        /// </summary>
        public const int Address = 54;

        /// <summary>
        /// Represents the payload type of the <see cref="Led1PulseTimePulseCounter"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Led1PulseTimePulseCounter"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led1PulseTimePulseCounter"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led1PulseTimePulseCounter"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led1PulseTimePulseCounter"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led1PulseTimePulseCounter"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led1PulseTimePulseCounter"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led1PulseTimePulseCounter"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led1PulseTimePulseCounter register.
    /// </summary>
    /// <seealso cref="Led1PulseTimePulseCounter"/>
    [Description("Filters and selects timestamped messages from the Led1PulseTimePulseCounter register.")]
    public partial class TimestampedLed1PulseTimePulseCounter
    {
        /// <summary>
        /// Represents the address of the <see cref="Led1PulseTimePulseCounter"/> register. This field is constant.
        /// </summary>
        public const int Address = Led1PulseTimePulseCounter.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led1PulseTimePulseCounter"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Led1PulseTimePulseCounter.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the wait time between pulses (milliseconds) of LED1 when in PulseTime mode, between 1 and 65535.
    /// </summary>
    [Description("Sets the wait time between pulses (milliseconds) of LED1 when in PulseTime mode, between 1 and 65535.")]
    public partial class Led1PulseTimeTail
    {
        /// <summary>
        /// Represents the address of the <see cref="Led1PulseTimeTail"/> register. This field is constant.
        /// </summary>
        public const int Address = 55;

        /// <summary>
        /// Represents the payload type of the <see cref="Led1PulseTimeTail"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Led1PulseTimeTail"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led1PulseTimeTail"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led1PulseTimeTail"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led1PulseTimeTail"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led1PulseTimeTail"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led1PulseTimeTail"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led1PulseTimeTail"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led1PulseTimeTail register.
    /// </summary>
    /// <seealso cref="Led1PulseTimeTail"/>
    [Description("Filters and selects timestamped messages from the Led1PulseTimeTail register.")]
    public partial class TimestampedLed1PulseTimeTail
    {
        /// <summary>
        /// Represents the address of the <see cref="Led1PulseTimeTail"/> register. This field is constant.
        /// </summary>
        public const int Address = Led1PulseTimeTail.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led1PulseTimeTail"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Led1PulseTimeTail.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the number of repetitions of LED1 pulse protocol when in PulseTime mode, between 1 and 65535.
    /// </summary>
    [Description("Sets the number of repetitions of LED1 pulse protocol when in PulseTime mode, between 1 and 65535.")]
    public partial class Led1PulseRepeatCounter
    {
        /// <summary>
        /// Represents the address of the <see cref="Led1PulseRepeatCounter"/> register. This field is constant.
        /// </summary>
        public const int Address = 56;

        /// <summary>
        /// Represents the payload type of the <see cref="Led1PulseRepeatCounter"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Led1PulseRepeatCounter"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led1PulseRepeatCounter"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led1PulseRepeatCounter"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led1PulseRepeatCounter"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led1PulseRepeatCounter"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led1PulseRepeatCounter"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led1PulseRepeatCounter"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led1PulseRepeatCounter register.
    /// </summary>
    /// <seealso cref="Led1PulseRepeatCounter"/>
    [Description("Filters and selects timestamped messages from the Led1PulseRepeatCounter register.")]
    public partial class TimestampedLed1PulseRepeatCounter
    {
        /// <summary>
        /// Represents the address of the <see cref="Led1PulseRepeatCounter"/> register. This field is constant.
        /// </summary>
        public const int Address = Led1PulseRepeatCounter.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led1PulseRepeatCounter"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Led1PulseRepeatCounter.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that get the real frequency (Hz) of LED0 when in Pwm mode.
    /// </summary>
    [Description("Get the real frequency (Hz) of LED0 when in Pwm mode.")]
    public partial class Led0PwmReal
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0PwmReal"/> register. This field is constant.
        /// </summary>
        public const int Address = 57;

        /// <summary>
        /// Represents the payload type of the <see cref="Led0PwmReal"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Led0PwmReal"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led0PwmReal"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led0PwmReal"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led0PwmReal"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0PwmReal"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led0PwmReal"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0PwmReal"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led0PwmReal register.
    /// </summary>
    /// <seealso cref="Led0PwmReal"/>
    [Description("Filters and selects timestamped messages from the Led0PwmReal register.")]
    public partial class TimestampedLed0PwmReal
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0PwmReal"/> register. This field is constant.
        /// </summary>
        public const int Address = Led0PwmReal.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led0PwmReal"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Led0PwmReal.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that get the real duty cycle (%) of LED0 when in Pwm mode.
    /// </summary>
    [Description("Get the real duty cycle (%) of LED0 when in Pwm mode.")]
    public partial class Led0PwmDutyCycleReal
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0PwmDutyCycleReal"/> register. This field is constant.
        /// </summary>
        public const int Address = 58;

        /// <summary>
        /// Represents the payload type of the <see cref="Led0PwmDutyCycleReal"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Led0PwmDutyCycleReal"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led0PwmDutyCycleReal"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led0PwmDutyCycleReal"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led0PwmDutyCycleReal"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0PwmDutyCycleReal"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led0PwmDutyCycleReal"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0PwmDutyCycleReal"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led0PwmDutyCycleReal register.
    /// </summary>
    /// <seealso cref="Led0PwmDutyCycleReal"/>
    [Description("Filters and selects timestamped messages from the Led0PwmDutyCycleReal register.")]
    public partial class TimestampedLed0PwmDutyCycleReal
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0PwmDutyCycleReal"/> register. This field is constant.
        /// </summary>
        public const int Address = Led0PwmDutyCycleReal.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led0PwmDutyCycleReal"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Led0PwmDutyCycleReal.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that get the real frequency (Hz) of LED1 when in Pwm mode.
    /// </summary>
    [Description("Get the real frequency (Hz) of LED1 when in Pwm mode.")]
    public partial class Led1PwmReal
    {
        /// <summary>
        /// Represents the address of the <see cref="Led1PwmReal"/> register. This field is constant.
        /// </summary>
        public const int Address = 59;

        /// <summary>
        /// Represents the payload type of the <see cref="Led1PwmReal"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Led1PwmReal"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led1PwmReal"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led1PwmReal"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led1PwmReal"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led1PwmReal"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led1PwmReal"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led1PwmReal"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led1PwmReal register.
    /// </summary>
    /// <seealso cref="Led1PwmReal"/>
    [Description("Filters and selects timestamped messages from the Led1PwmReal register.")]
    public partial class TimestampedLed1PwmReal
    {
        /// <summary>
        /// Represents the address of the <see cref="Led1PwmReal"/> register. This field is constant.
        /// </summary>
        public const int Address = Led1PwmReal.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led1PwmReal"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Led1PwmReal.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that get the real duty cycle (%) of LED1 when in Pwm mode.
    /// </summary>
    [Description("Get the real duty cycle (%) of LED1 when in Pwm mode.")]
    public partial class LedD1PwmDutyCycleReal
    {
        /// <summary>
        /// Represents the address of the <see cref="LedD1PwmDutyCycleReal"/> register. This field is constant.
        /// </summary>
        public const int Address = 60;

        /// <summary>
        /// Represents the payload type of the <see cref="LedD1PwmDutyCycleReal"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="LedD1PwmDutyCycleReal"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LedD1PwmDutyCycleReal"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LedD1PwmDutyCycleReal"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LedD1PwmDutyCycleReal"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LedD1PwmDutyCycleReal"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LedD1PwmDutyCycleReal"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LedD1PwmDutyCycleReal"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LedD1PwmDutyCycleReal register.
    /// </summary>
    /// <seealso cref="LedD1PwmDutyCycleReal"/>
    [Description("Filters and selects timestamped messages from the LedD1PwmDutyCycleReal register.")]
    public partial class TimestampedLedD1PwmDutyCycleReal
    {
        /// <summary>
        /// Represents the address of the <see cref="LedD1PwmDutyCycleReal"/> register. This field is constant.
        /// </summary>
        public const int Address = LedD1PwmDutyCycleReal.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LedD1PwmDutyCycleReal"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return LedD1PwmDutyCycleReal.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that write the state of the auxiliary digital output bit.
    /// </summary>
    [Description("Write the state of the auxiliary digital output bit.")]
    public partial class AuxDigitalOutputState
    {
        /// <summary>
        /// Represents the address of the <see cref="AuxDigitalOutputState"/> register. This field is constant.
        /// </summary>
        public const int Address = 61;

        /// <summary>
        /// Represents the payload type of the <see cref="AuxDigitalOutputState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="AuxDigitalOutputState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="AuxDigitalOutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static AuxDigitalOutputs GetPayload(HarpMessage message)
        {
            return (AuxDigitalOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="AuxDigitalOutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AuxDigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((AuxDigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="AuxDigitalOutputState"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AuxDigitalOutputState"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, AuxDigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="AuxDigitalOutputState"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AuxDigitalOutputState"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AuxDigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// AuxDigitalOutputState register.
    /// </summary>
    /// <seealso cref="AuxDigitalOutputState"/>
    [Description("Filters and selects timestamped messages from the AuxDigitalOutputState register.")]
    public partial class TimestampedAuxDigitalOutputState
    {
        /// <summary>
        /// Represents the address of the <see cref="AuxDigitalOutputState"/> register. This field is constant.
        /// </summary>
        public const int Address = AuxDigitalOutputState.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="AuxDigitalOutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AuxDigitalOutputs> GetPayload(HarpMessage message)
        {
            return AuxDigitalOutputState.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the power to be applied to auxiliary LED, between 1 and 120.
    /// </summary>
    [Description("Sets the power to be applied to auxiliary LED, between 1 and 120.")]
    public partial class AuxLedPower
    {
        /// <summary>
        /// Represents the address of the <see cref="AuxLedPower"/> register. This field is constant.
        /// </summary>
        public const int Address = 62;

        /// <summary>
        /// Represents the payload type of the <see cref="AuxLedPower"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="AuxLedPower"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="AuxLedPower"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="AuxLedPower"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="AuxLedPower"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AuxLedPower"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="AuxLedPower"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AuxLedPower"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// AuxLedPower register.
    /// </summary>
    /// <seealso cref="AuxLedPower"/>
    [Description("Filters and selects timestamped messages from the AuxLedPower register.")]
    public partial class TimestampedAuxLedPower
    {
        /// <summary>
        /// Represents the address of the <see cref="AuxLedPower"/> register. This field is constant.
        /// </summary>
        public const int Address = AuxLedPower.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="AuxLedPower"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return AuxLedPower.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that write the state of digital output lines.
    /// </summary>
    [Description("Write the state of digital output lines.")]
    public partial class DigitalOutputState
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalOutputState"/> register. This field is constant.
        /// </summary>
        public const int Address = 63;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalOutputState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DigitalOutputState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DigitalOutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalOutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalOutputState"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalOutputState"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalOutputState"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalOutputState"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalOutputState register.
    /// </summary>
    /// <seealso cref="DigitalOutputState"/>
    [Description("Filters and selects timestamped messages from the DigitalOutputState register.")]
    public partial class TimestampedDigitalOutputState
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalOutputState"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalOutputState.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalOutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return DigitalOutputState.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies all the active events in the device.
    /// </summary>
    [Description("Specifies all the active events in the device.")]
    public partial class EnableEvents
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableEvents"/> register. This field is constant.
        /// </summary>
        public const int Address = 65;

        /// <summary>
        /// Represents the payload type of the <see cref="EnableEvents"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EnableEvents"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EnableEvents"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static LedArrayEvents GetPayload(HarpMessage message)
        {
            return (LedArrayEvents)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EnableEvents"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LedArrayEvents> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((LedArrayEvents)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EnableEvents"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableEvents"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, LedArrayEvents value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EnableEvents"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableEvents"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, LedArrayEvents value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EnableEvents register.
    /// </summary>
    /// <seealso cref="EnableEvents"/>
    [Description("Filters and selects timestamped messages from the EnableEvents register.")]
    public partial class TimestampedEnableEvents
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableEvents"/> register. This field is constant.
        /// </summary>
        public const int Address = EnableEvents.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EnableEvents"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LedArrayEvents> GetPayload(HarpMessage message)
        {
            return EnableEvents.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents an operator which creates standard message payloads for the
    /// LedArray device.
    /// </summary>
    /// <seealso cref="CreateEnablePowerPayload"/>
    /// <seealso cref="CreateEnableLedModePayload"/>
    /// <seealso cref="CreateEnableLedPayload"/>
    /// <seealso cref="CreateDigitalInputStatePayload"/>
    /// <seealso cref="CreateDigitalOutputSyncPayload"/>
    /// <seealso cref="CreateDigitalInputTriggerPayload"/>
    /// <seealso cref="CreatePulseModePayload"/>
    /// <seealso cref="CreateLed0PowerPayload"/>
    /// <seealso cref="CreateLed1PowerPayload"/>
    /// <seealso cref="CreateLed0PwmFrequencyPayload"/>
    /// <seealso cref="CreateLed0PwmDutyCyclePayload"/>
    /// <seealso cref="CreateLed0PwmPulseCounterPayload"/>
    /// <seealso cref="CreateLed0PulseTimeOnPayload"/>
    /// <seealso cref="CreateLed0PulseTimeOffPayload"/>
    /// <seealso cref="CreateLed0PulseTimePulseCounterPayload"/>
    /// <seealso cref="CreateLed0PulseTimeTailPayload"/>
    /// <seealso cref="CreateLed0PulseRepeatCounterPayload"/>
    /// <seealso cref="CreateLed1PwmFrequencyPayload"/>
    /// <seealso cref="CreateLed1PwmDutyCyclePayload"/>
    /// <seealso cref="CreateLed1PwmPulseCounterPayload"/>
    /// <seealso cref="CreateLed1PulseTimeOnPayload"/>
    /// <seealso cref="CreateLed1PulseTimeOffPayload"/>
    /// <seealso cref="CreateLed1PulseTimePulseCounterPayload"/>
    /// <seealso cref="CreateLed1PulseTimeTailPayload"/>
    /// <seealso cref="CreateLed1PulseRepeatCounterPayload"/>
    /// <seealso cref="CreateLed0PwmRealPayload"/>
    /// <seealso cref="CreateLed0PwmDutyCycleRealPayload"/>
    /// <seealso cref="CreateLed1PwmRealPayload"/>
    /// <seealso cref="CreateLedD1PwmDutyCycleRealPayload"/>
    /// <seealso cref="CreateAuxDigitalOutputStatePayload"/>
    /// <seealso cref="CreateAuxLedPowerPayload"/>
    /// <seealso cref="CreateDigitalOutputStatePayload"/>
    /// <seealso cref="CreateEnableEventsPayload"/>
    [XmlInclude(typeof(CreateEnablePowerPayload))]
    [XmlInclude(typeof(CreateEnableLedModePayload))]
    [XmlInclude(typeof(CreateEnableLedPayload))]
    [XmlInclude(typeof(CreateDigitalInputStatePayload))]
    [XmlInclude(typeof(CreateDigitalOutputSyncPayload))]
    [XmlInclude(typeof(CreateDigitalInputTriggerPayload))]
    [XmlInclude(typeof(CreatePulseModePayload))]
    [XmlInclude(typeof(CreateLed0PowerPayload))]
    [XmlInclude(typeof(CreateLed1PowerPayload))]
    [XmlInclude(typeof(CreateLed0PwmFrequencyPayload))]
    [XmlInclude(typeof(CreateLed0PwmDutyCyclePayload))]
    [XmlInclude(typeof(CreateLed0PwmPulseCounterPayload))]
    [XmlInclude(typeof(CreateLed0PulseTimeOnPayload))]
    [XmlInclude(typeof(CreateLed0PulseTimeOffPayload))]
    [XmlInclude(typeof(CreateLed0PulseTimePulseCounterPayload))]
    [XmlInclude(typeof(CreateLed0PulseTimeTailPayload))]
    [XmlInclude(typeof(CreateLed0PulseRepeatCounterPayload))]
    [XmlInclude(typeof(CreateLed1PwmFrequencyPayload))]
    [XmlInclude(typeof(CreateLed1PwmDutyCyclePayload))]
    [XmlInclude(typeof(CreateLed1PwmPulseCounterPayload))]
    [XmlInclude(typeof(CreateLed1PulseTimeOnPayload))]
    [XmlInclude(typeof(CreateLed1PulseTimeOffPayload))]
    [XmlInclude(typeof(CreateLed1PulseTimePulseCounterPayload))]
    [XmlInclude(typeof(CreateLed1PulseTimeTailPayload))]
    [XmlInclude(typeof(CreateLed1PulseRepeatCounterPayload))]
    [XmlInclude(typeof(CreateLed0PwmRealPayload))]
    [XmlInclude(typeof(CreateLed0PwmDutyCycleRealPayload))]
    [XmlInclude(typeof(CreateLed1PwmRealPayload))]
    [XmlInclude(typeof(CreateLedD1PwmDutyCycleRealPayload))]
    [XmlInclude(typeof(CreateAuxDigitalOutputStatePayload))]
    [XmlInclude(typeof(CreateAuxLedPowerPayload))]
    [XmlInclude(typeof(CreateDigitalOutputStatePayload))]
    [XmlInclude(typeof(CreateEnableEventsPayload))]
    [Description("Creates standard message payloads for the LedArray device.")]
    public partial class CreateMessage : CreateMessageBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMessage"/> class.
        /// </summary>
        public CreateMessage()
        {
            Payload = new CreateEnablePowerPayload();
        }

        string INamedElement.Name => $"{nameof(LedArray)}.{GetElementDisplayName(Payload)}";
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that control the enable of both LEDs' power supply.
    /// </summary>
    [DisplayName("EnablePowerPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that control the enable of both LEDs' power supply.")]
    public partial class CreateEnablePowerPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that control the enable of both LEDs' power supply.
        /// </summary>
        [Description("The value that control the enable of both LEDs' power supply.")]
        public LedState Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that control the enable of both LEDs' power supply.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that control the enable of both LEDs' power supply.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => EnablePower.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that start/stop the LEDs according to the pulse configuration.
    /// </summary>
    [DisplayName("EnableLedModePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that start/stop the LEDs according to the pulse configuration.")]
    public partial class CreateEnableLedModePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that start/stop the LEDs according to the pulse configuration.
        /// </summary>
        [Description("The value that start/stop the LEDs according to the pulse configuration.")]
        public LedState Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that start/stop the LEDs according to the pulse configuration.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that start/stop the LEDs according to the pulse configuration.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => EnableLedMode.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that enables/disables the LEDs.
    /// </summary>
    [DisplayName("EnableLedPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that enables/disables the LEDs.")]
    public partial class CreateEnableLedPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that enables/disables the LEDs.
        /// </summary>
        [Description("The value that enables/disables the LEDs.")]
        public LedState Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that enables/disables the LEDs.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that enables/disables the LEDs.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => EnableLed.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that state of the digital input pins. An event will be emitted when the value of any digital input pin changes.
    /// </summary>
    [DisplayName("DigitalInputStatePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that state of the digital input pins. An event will be emitted when the value of any digital input pin changes.")]
    public partial class CreateDigitalInputStatePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that state of the digital input pins. An event will be emitted when the value of any digital input pin changes.
        /// </summary>
        [Description("The value that state of the digital input pins. An event will be emitted when the value of any digital input pin changes.")]
        public DigitalInputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that state of the digital input pins. An event will be emitted when the value of any digital input pin changes.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that state of the digital input pins. An event will be emitted when the value of any digital input pin changes.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DigitalInputState.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that configuration of the digital outputs behavior.
    /// </summary>
    [DisplayName("DigitalOutputSyncPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that configuration of the digital outputs behavior.")]
    public partial class CreateDigitalOutputSyncPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets a value that configuration of the DO0 functionality.
        /// </summary>
        [Description("Configuration of the DO0 functionality.")]
        public DO0SyncConfig DO0Sync { get; set; }

        /// <summary>
        /// Gets or sets a value that configuration of the DO1 functionality.
        /// </summary>
        [Description("Configuration of the DO1 functionality.")]
        public DO1SyncConfig DO1Sync { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that configuration of the digital outputs behavior.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that configuration of the digital outputs behavior.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ =>
            {
                DigitalOutputSyncPayload value;
                value.DO0Sync = DO0Sync;
                value.DO1Sync = DO1Sync;
                return DigitalOutputSync.FromPayload(MessageType, value);
            });
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that configuration of the digital inputs pins behavior.
    /// </summary>
    [DisplayName("DigitalInputTriggerPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that configuration of the digital inputs pins behavior.")]
    public partial class CreateDigitalInputTriggerPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets a value that configuration of the DIO input pin.
        /// </summary>
        [Description("Configuration of the DIO input pin.")]
        public DigitalInputTriggerConfig DI0Trigger { get; set; }

        /// <summary>
        /// Gets or sets a value that configuration of the DI1 input pin.
        /// </summary>
        [Description("Configuration of the DI1 input pin.")]
        public DigitalInputTriggerConfig DI1Trigger { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that configuration of the digital inputs pins behavior.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that configuration of the digital inputs pins behavior.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ =>
            {
                DigitalInputTriggerPayload value;
                value.DI0Trigger = DI0Trigger;
                value.DI1Trigger = DI1Trigger;
                return DigitalInputTrigger.FromPayload(MessageType, value);
            });
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the pulse mode used by the LEDs.
    /// </summary>
    [DisplayName("PulseModePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the pulse mode used by the LEDs.")]
    public partial class CreatePulseModePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets a value that sets the pulse mode used in LED0.
        /// </summary>
        [Description("Sets the pulse mode used in LED0.")]
        public PulseModeConfig Led0Mode { get; set; }

        /// <summary>
        /// Gets or sets a value that sets the pulse mode used in LED0.
        /// </summary>
        [Description("Sets the pulse mode used in LED0")]
        public PulseModeConfig Led1Mode { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the pulse mode used by the LEDs.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the pulse mode used by the LEDs.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ =>
            {
                PulseModePayload value;
                value.Led0Mode = Led0Mode;
                value.Led1Mode = Led1Mode;
                return PulseMode.FromPayload(MessageType, value);
            });
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the power to LED0, between 1 and 120 (arbitrary units).
    /// </summary>
    [DisplayName("Led0PowerPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the power to LED0, between 1 and 120 (arbitrary units).")]
    public partial class CreateLed0PowerPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the power to LED0, between 1 and 120 (arbitrary units).
        /// </summary>
        [Range(min: 1, max: 120)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the power to LED0, between 1 and 120 (arbitrary units).")]
        public byte Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the power to LED0, between 1 and 120 (arbitrary units).
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the power to LED0, between 1 and 120 (arbitrary units).
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led0Power.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the power to LED1, between 1 and 120 (arbitrary units).
    /// </summary>
    [DisplayName("Led1PowerPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the power to LED1, between 1 and 120 (arbitrary units).")]
    public partial class CreateLed1PowerPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the power to LED1, between 1 and 120 (arbitrary units).
        /// </summary>
        [Range(min: 1, max: 120)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the power to LED1, between 1 and 120 (arbitrary units).")]
        public byte Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the power to LED1, between 1 and 120 (arbitrary units).
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the power to LED1, between 1 and 120 (arbitrary units).
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led1Power.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the frequency (Hz) of LED0 when in Pwm mode, between 0.5 and 2000.
    /// </summary>
    [DisplayName("Led0PwmFrequencyPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the frequency (Hz) of LED0 when in Pwm mode, between 0.5 and 2000.")]
    public partial class CreateLed0PwmFrequencyPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the frequency (Hz) of LED0 when in Pwm mode, between 0.5 and 2000.
        /// </summary>
        [Range(min: 0.5, max: 2000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the frequency (Hz) of LED0 when in Pwm mode, between 0.5 and 2000.")]
        public float Value { get; set; } = 0.5;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the frequency (Hz) of LED0 when in Pwm mode, between 0.5 and 2000.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the frequency (Hz) of LED0 when in Pwm mode, between 0.5 and 2000.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led0PwmFrequency.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the duty cycle (%) of LED0 when in Pwm mode, between 0.1 and 99.9.
    /// </summary>
    [DisplayName("Led0PwmDutyCyclePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the duty cycle (%) of LED0 when in Pwm mode, between 0.1 and 99.9.")]
    public partial class CreateLed0PwmDutyCyclePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the duty cycle (%) of LED0 when in Pwm mode, between 0.1 and 99.9.
        /// </summary>
        [Range(min: 0.1, max: 99.9)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the duty cycle (%) of LED0 when in Pwm mode, between 0.1 and 99.9.")]
        public float Value { get; set; } = 0.1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the duty cycle (%) of LED0 when in Pwm mode, between 0.1 and 99.9.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the duty cycle (%) of LED0 when in Pwm mode, between 0.1 and 99.9.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led0PwmDutyCycle.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the number of pulses of LED0 when in Pwm mode, between 1 and 65535.
    /// </summary>
    [DisplayName("Led0PwmPulseCounterPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the number of pulses of LED0 when in Pwm mode, between 1 and 65535.")]
    public partial class CreateLed0PwmPulseCounterPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the number of pulses of LED0 when in Pwm mode, between 1 and 65535.
        /// </summary>
        [Range(min: 1, max: 65535)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the number of pulses of LED0 when in Pwm mode, between 1 and 65535.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the number of pulses of LED0 when in Pwm mode, between 1 and 65535.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the number of pulses of LED0 when in Pwm mode, between 1 and 65535.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led0PwmPulseCounter.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the time on (milliseconds) of LED0 when in PulseTime mode, between 1 and 65535.
    /// </summary>
    [DisplayName("Led0PulseTimeOnPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the time on (milliseconds) of LED0 when in PulseTime mode, between 1 and 65535.")]
    public partial class CreateLed0PulseTimeOnPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the time on (milliseconds) of LED0 when in PulseTime mode, between 1 and 65535.
        /// </summary>
        [Range(min: 1, max: 65535)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the time on (milliseconds) of LED0 when in PulseTime mode, between 1 and 65535.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the time on (milliseconds) of LED0 when in PulseTime mode, between 1 and 65535.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the time on (milliseconds) of LED0 when in PulseTime mode, between 1 and 65535.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led0PulseTimeOn.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the time off (milliseconds) of LED0 when in PulseTime mode, between 1 and 65535.
    /// </summary>
    [DisplayName("Led0PulseTimeOffPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the time off (milliseconds) of LED0 when in PulseTime mode, between 1 and 65535.")]
    public partial class CreateLed0PulseTimeOffPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the time off (milliseconds) of LED0 when in PulseTime mode, between 1 and 65535.
        /// </summary>
        [Range(min: 1, max: 65535)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the time off (milliseconds) of LED0 when in PulseTime mode, between 1 and 65535.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the time off (milliseconds) of LED0 when in PulseTime mode, between 1 and 65535.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the time off (milliseconds) of LED0 when in PulseTime mode, between 1 and 65535.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led0PulseTimeOff.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the number of pulses of LED0 when in PulseTime mode, between 1 and 65535.
    /// </summary>
    [DisplayName("Led0PulseTimePulseCounterPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the number of pulses of LED0 when in PulseTime mode, between 1 and 65535.")]
    public partial class CreateLed0PulseTimePulseCounterPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the number of pulses of LED0 when in PulseTime mode, between 1 and 65535.
        /// </summary>
        [Range(min: 1, max: 65535)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the number of pulses of LED0 when in PulseTime mode, between 1 and 65535.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the number of pulses of LED0 when in PulseTime mode, between 1 and 65535.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the number of pulses of LED0 when in PulseTime mode, between 1 and 65535.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led0PulseTimePulseCounter.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the wait time between pulses (milliseconds) of LED0 when in PulseTime mode, between 1 and 65535.
    /// </summary>
    [DisplayName("Led0PulseTimeTailPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the wait time between pulses (milliseconds) of LED0 when in PulseTime mode, between 1 and 65535.")]
    public partial class CreateLed0PulseTimeTailPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the wait time between pulses (milliseconds) of LED0 when in PulseTime mode, between 1 and 65535.
        /// </summary>
        [Range(min: 1, max: 65535)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the wait time between pulses (milliseconds) of LED0 when in PulseTime mode, between 1 and 65535.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the wait time between pulses (milliseconds) of LED0 when in PulseTime mode, between 1 and 65535.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the wait time between pulses (milliseconds) of LED0 when in PulseTime mode, between 1 and 65535.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led0PulseTimeTail.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the number of repetitions of LED0 pulse protocol when in PulseTime mode, between 1 and 65535.
    /// </summary>
    [DisplayName("Led0PulseRepeatCounterPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the number of repetitions of LED0 pulse protocol when in PulseTime mode, between 1 and 65535.")]
    public partial class CreateLed0PulseRepeatCounterPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the number of repetitions of LED0 pulse protocol when in PulseTime mode, between 1 and 65535.
        /// </summary>
        [Range(min: 1, max: 65535)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the number of repetitions of LED0 pulse protocol when in PulseTime mode, between 1 and 65535.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the number of repetitions of LED0 pulse protocol when in PulseTime mode, between 1 and 65535.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the number of repetitions of LED0 pulse protocol when in PulseTime mode, between 1 and 65535.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led0PulseRepeatCounter.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the frequency (Hz) of LED1 when in Pwm mode, between 0.5 and 2000.
    /// </summary>
    [DisplayName("Led1PwmFrequencyPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the frequency (Hz) of LED1 when in Pwm mode, between 0.5 and 2000.")]
    public partial class CreateLed1PwmFrequencyPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the frequency (Hz) of LED1 when in Pwm mode, between 0.5 and 2000.
        /// </summary>
        [Range(min: 0.5, max: 2000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the frequency (Hz) of LED1 when in Pwm mode, between 0.5 and 2000.")]
        public float Value { get; set; } = 0.5;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the frequency (Hz) of LED1 when in Pwm mode, between 0.5 and 2000.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the frequency (Hz) of LED1 when in Pwm mode, between 0.5 and 2000.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led1PwmFrequency.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the duty cycle (%) of LED1 when in Pwm mode, between 0.1 and 99.9.
    /// </summary>
    [DisplayName("Led1PwmDutyCyclePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the duty cycle (%) of LED1 when in Pwm mode, between 0.1 and 99.9.")]
    public partial class CreateLed1PwmDutyCyclePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the duty cycle (%) of LED1 when in Pwm mode, between 0.1 and 99.9.
        /// </summary>
        [Range(min: 0.1, max: 99.9)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the duty cycle (%) of LED1 when in Pwm mode, between 0.1 and 99.9.")]
        public float Value { get; set; } = 0.1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the duty cycle (%) of LED1 when in Pwm mode, between 0.1 and 99.9.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the duty cycle (%) of LED1 when in Pwm mode, between 0.1 and 99.9.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led1PwmDutyCycle.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the number of pulses of LED1 when in Pwm mode, between 1 and 65535.
    /// </summary>
    [DisplayName("Led1PwmPulseCounterPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the number of pulses of LED1 when in Pwm mode, between 1 and 65535.")]
    public partial class CreateLed1PwmPulseCounterPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the number of pulses of LED1 when in Pwm mode, between 1 and 65535.
        /// </summary>
        [Range(min: 1, max: 65535)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the number of pulses of LED1 when in Pwm mode, between 1 and 65535.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the number of pulses of LED1 when in Pwm mode, between 1 and 65535.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the number of pulses of LED1 when in Pwm mode, between 1 and 65535.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led1PwmPulseCounter.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the time on (milliseconds) of LED1 when in PulseTime mode, between 1 and 65535.
    /// </summary>
    [DisplayName("Led1PulseTimeOnPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the time on (milliseconds) of LED1 when in PulseTime mode, between 1 and 65535.")]
    public partial class CreateLed1PulseTimeOnPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the time on (milliseconds) of LED1 when in PulseTime mode, between 1 and 65535.
        /// </summary>
        [Range(min: 1, max: 65535)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the time on (milliseconds) of LED1 when in PulseTime mode, between 1 and 65535.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the time on (milliseconds) of LED1 when in PulseTime mode, between 1 and 65535.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the time on (milliseconds) of LED1 when in PulseTime mode, between 1 and 65535.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led1PulseTimeOn.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the time off (milliseconds) of LED1 when in PulseTime mode, between 1 and 65535.
    /// </summary>
    [DisplayName("Led1PulseTimeOffPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the time off (milliseconds) of LED1 when in PulseTime mode, between 1 and 65535.")]
    public partial class CreateLed1PulseTimeOffPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the time off (milliseconds) of LED1 when in PulseTime mode, between 1 and 65535.
        /// </summary>
        [Range(min: 1, max: 65535)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the time off (milliseconds) of LED1 when in PulseTime mode, between 1 and 65535.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the time off (milliseconds) of LED1 when in PulseTime mode, between 1 and 65535.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the time off (milliseconds) of LED1 when in PulseTime mode, between 1 and 65535.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led1PulseTimeOff.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the number of pulses of LED1 when in PulseTime mode, between 1 and 65535.
    /// </summary>
    [DisplayName("Led1PulseTimePulseCounterPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the number of pulses of LED1 when in PulseTime mode, between 1 and 65535.")]
    public partial class CreateLed1PulseTimePulseCounterPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the number of pulses of LED1 when in PulseTime mode, between 1 and 65535.
        /// </summary>
        [Range(min: 1, max: 65535)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the number of pulses of LED1 when in PulseTime mode, between 1 and 65535.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the number of pulses of LED1 when in PulseTime mode, between 1 and 65535.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the number of pulses of LED1 when in PulseTime mode, between 1 and 65535.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led1PulseTimePulseCounter.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the wait time between pulses (milliseconds) of LED1 when in PulseTime mode, between 1 and 65535.
    /// </summary>
    [DisplayName("Led1PulseTimeTailPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the wait time between pulses (milliseconds) of LED1 when in PulseTime mode, between 1 and 65535.")]
    public partial class CreateLed1PulseTimeTailPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the wait time between pulses (milliseconds) of LED1 when in PulseTime mode, between 1 and 65535.
        /// </summary>
        [Range(min: 1, max: 65535)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the wait time between pulses (milliseconds) of LED1 when in PulseTime mode, between 1 and 65535.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the wait time between pulses (milliseconds) of LED1 when in PulseTime mode, between 1 and 65535.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the wait time between pulses (milliseconds) of LED1 when in PulseTime mode, between 1 and 65535.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led1PulseTimeTail.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the number of repetitions of LED1 pulse protocol when in PulseTime mode, between 1 and 65535.
    /// </summary>
    [DisplayName("Led1PulseRepeatCounterPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the number of repetitions of LED1 pulse protocol when in PulseTime mode, between 1 and 65535.")]
    public partial class CreateLed1PulseRepeatCounterPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the number of repetitions of LED1 pulse protocol when in PulseTime mode, between 1 and 65535.
        /// </summary>
        [Range(min: 1, max: 65535)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the number of repetitions of LED1 pulse protocol when in PulseTime mode, between 1 and 65535.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the number of repetitions of LED1 pulse protocol when in PulseTime mode, between 1 and 65535.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the number of repetitions of LED1 pulse protocol when in PulseTime mode, between 1 and 65535.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led1PulseRepeatCounter.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that get the real frequency (Hz) of LED0 when in Pwm mode.
    /// </summary>
    [DisplayName("Led0PwmRealPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that get the real frequency (Hz) of LED0 when in Pwm mode.")]
    public partial class CreateLed0PwmRealPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that get the real frequency (Hz) of LED0 when in Pwm mode.
        /// </summary>
        [Description("The value that get the real frequency (Hz) of LED0 when in Pwm mode.")]
        public float Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that get the real frequency (Hz) of LED0 when in Pwm mode.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that get the real frequency (Hz) of LED0 when in Pwm mode.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led0PwmReal.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that get the real duty cycle (%) of LED0 when in Pwm mode.
    /// </summary>
    [DisplayName("Led0PwmDutyCycleRealPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that get the real duty cycle (%) of LED0 when in Pwm mode.")]
    public partial class CreateLed0PwmDutyCycleRealPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that get the real duty cycle (%) of LED0 when in Pwm mode.
        /// </summary>
        [Description("The value that get the real duty cycle (%) of LED0 when in Pwm mode.")]
        public float Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that get the real duty cycle (%) of LED0 when in Pwm mode.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that get the real duty cycle (%) of LED0 when in Pwm mode.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led0PwmDutyCycleReal.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that get the real frequency (Hz) of LED1 when in Pwm mode.
    /// </summary>
    [DisplayName("Led1PwmRealPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that get the real frequency (Hz) of LED1 when in Pwm mode.")]
    public partial class CreateLed1PwmRealPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that get the real frequency (Hz) of LED1 when in Pwm mode.
        /// </summary>
        [Description("The value that get the real frequency (Hz) of LED1 when in Pwm mode.")]
        public float Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that get the real frequency (Hz) of LED1 when in Pwm mode.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that get the real frequency (Hz) of LED1 when in Pwm mode.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led1PwmReal.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that get the real duty cycle (%) of LED1 when in Pwm mode.
    /// </summary>
    [DisplayName("LedD1PwmDutyCycleRealPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that get the real duty cycle (%) of LED1 when in Pwm mode.")]
    public partial class CreateLedD1PwmDutyCycleRealPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that get the real duty cycle (%) of LED1 when in Pwm mode.
        /// </summary>
        [Description("The value that get the real duty cycle (%) of LED1 when in Pwm mode.")]
        public float Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that get the real duty cycle (%) of LED1 when in Pwm mode.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that get the real duty cycle (%) of LED1 when in Pwm mode.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => LedD1PwmDutyCycleReal.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that write the state of the auxiliary digital output bit.
    /// </summary>
    [DisplayName("AuxDigitalOutputStatePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that write the state of the auxiliary digital output bit.")]
    public partial class CreateAuxDigitalOutputStatePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that write the state of the auxiliary digital output bit.
        /// </summary>
        [Description("The value that write the state of the auxiliary digital output bit.")]
        public AuxDigitalOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that write the state of the auxiliary digital output bit.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that write the state of the auxiliary digital output bit.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => AuxDigitalOutputState.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the power to be applied to auxiliary LED, between 1 and 120.
    /// </summary>
    [DisplayName("AuxLedPowerPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the power to be applied to auxiliary LED, between 1 and 120.")]
    public partial class CreateAuxLedPowerPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the power to be applied to auxiliary LED, between 1 and 120.
        /// </summary>
        [Range(min: 1, max: 120)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the power to be applied to auxiliary LED, between 1 and 120.")]
        public byte Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the power to be applied to auxiliary LED, between 1 and 120.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the power to be applied to auxiliary LED, between 1 and 120.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => AuxLedPower.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that write the state of digital output lines.
    /// </summary>
    [DisplayName("DigitalOutputStatePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that write the state of digital output lines.")]
    public partial class CreateDigitalOutputStatePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that write the state of digital output lines.
        /// </summary>
        [Description("The value that write the state of digital output lines.")]
        public DigitalOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that write the state of digital output lines.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that write the state of digital output lines.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DigitalOutputState.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies all the active events in the device.
    /// </summary>
    [DisplayName("EnableEventsPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies all the active events in the device.")]
    public partial class CreateEnableEventsPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies all the active events in the device.
        /// </summary>
        [Description("The value that specifies all the active events in the device.")]
        public LedArrayEvents Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies all the active events in the device.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies all the active events in the device.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => EnableEvents.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents the payload of the DigitalOutputSync register.
    /// </summary>
    public struct DigitalOutputSyncPayload
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DigitalOutputSyncPayload"/> structure.
        /// </summary>
        /// <param name="dO0Sync">Configuration of the DO0 functionality.</param>
        /// <param name="dO1Sync">Configuration of the DO1 functionality.</param>
        public DigitalOutputSyncPayload(
            DO0SyncConfig dO0Sync,
            DO1SyncConfig dO1Sync)
        {
            DO0Sync = dO0Sync;
            DO1Sync = dO1Sync;
        }

        /// <summary>
        /// Configuration of the DO0 functionality.
        /// </summary>
        public DO0SyncConfig DO0Sync;

        /// <summary>
        /// Configuration of the DO1 functionality.
        /// </summary>
        public DO1SyncConfig DO1Sync;
    }

    /// <summary>
    /// Represents the payload of the DigitalInputTrigger register.
    /// </summary>
    public struct DigitalInputTriggerPayload
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DigitalInputTriggerPayload"/> structure.
        /// </summary>
        /// <param name="dI0Trigger">Configuration of the DIO input pin.</param>
        /// <param name="dI1Trigger">Configuration of the DI1 input pin.</param>
        public DigitalInputTriggerPayload(
            DigitalInputTriggerConfig dI0Trigger,
            DigitalInputTriggerConfig dI1Trigger)
        {
            DI0Trigger = dI0Trigger;
            DI1Trigger = dI1Trigger;
        }

        /// <summary>
        /// Configuration of the DIO input pin.
        /// </summary>
        public DigitalInputTriggerConfig DI0Trigger;

        /// <summary>
        /// Configuration of the DI1 input pin.
        /// </summary>
        public DigitalInputTriggerConfig DI1Trigger;
    }

    /// <summary>
    /// Represents the payload of the PulseMode register.
    /// </summary>
    public struct PulseModePayload
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PulseModePayload"/> structure.
        /// </summary>
        /// <param name="led0Mode">Sets the pulse mode used in LED0.</param>
        /// <param name="led1Mode">Sets the pulse mode used in LED0</param>
        public PulseModePayload(
            PulseModeConfig led0Mode,
            PulseModeConfig led1Mode)
        {
            Led0Mode = led0Mode;
            Led1Mode = led1Mode;
        }

        /// <summary>
        /// Sets the pulse mode used in LED0.
        /// </summary>
        public PulseModeConfig Led0Mode;

        /// <summary>
        /// Sets the pulse mode used in LED0
        /// </summary>
        public PulseModeConfig Led1Mode;
    }

    /// <summary>
    /// Specifies the LEDs state.
    /// </summary>
    [Flags]
    public enum LedState : byte
    {
        Led0On = 0x1,
        Led1On = 0x2,
        Led0Off = 0x4,
        Led1Off = 0x8
    }

    /// <summary>
    /// Specifies the state of port digital input lines.
    /// </summary>
    [Flags]
    public enum DigitalInputs : byte
    {
        DI0 = 0x1,
        DI1 = 0x2
    }

    /// <summary>
    /// Specifies the state of the auxiliary digital output lines.
    /// </summary>
    [Flags]
    public enum AuxDigitalOutputs : byte
    {
        Aux0Set = 0x1,
        Aux1Set = 0x2,
        Aux0Clear = 0x4,
        Aux1Clear = 0x8
    }

    /// <summary>
    /// Specifies the state of port digital output lines.
    /// </summary>
    [Flags]
    public enum DigitalOutputs : byte
    {
        DO0Set = 0x1,
        DO1Set = 0x2,
        DO0Clear = 0x4,
        DO1Clear = 0x8
    }

    /// <summary>
    /// The events that can be enabled/disabled.
    /// </summary>
    [Flags]
    public enum LedArrayEvents : byte
    {
        EnableLed = 0x1,
        DigitalInputState = 0x2
    }

    /// <summary>
    /// Available configurations when using digital output pin 0 to report firmware events.
    /// </summary>
    public enum DO0SyncConfig : byte
    {
        None = 0,
        MimicLed0EnablePower = 1,
        MimicLed0EnableBehavior = 2,
        MimicLed0EnableLed = 3
    }

    /// <summary>
    /// Available configurations when using digital output pin 1 to report firmware events.
    /// </summary>
    public enum DO1SyncConfig : byte
    {
        None = 0,
        MimicLed1EnablePower = 16,
        MimicLed1EnableBehavior = 32,
        MimicLed1EnableLed = 48
    }

    /// <summary>
    /// Available configurations when using digital inputs as an acquisition trigger.
    /// </summary>
    public enum DigitalInputTriggerConfig : byte
    {
        Led0EnablePower = 0,
        Led0EnableBehavior = 1,
        Led0EnableLed = 2,
        Led1EnablePower = 3,
        Led1EnableBehavior = 4,
        Led1EnableLed = 5,
        None = 6
    }

    /// <summary>
    /// Available configurations modes when LED behavior is enabled.
    /// </summary>
    public enum PulseModeConfig : byte
    {
        Pwm = 0,
        PulseTime = 1
    }
}
