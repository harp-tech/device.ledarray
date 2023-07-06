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
            { 33, typeof(EnableBehavior) },
            { 34, typeof(EnableLED) },
            { 35, typeof(DigitalInputState) },
            { 36, typeof(DigitalOutputSync) },
            { 37, typeof(DigitalInputTrigger) },
            { 38, typeof(LEDMode) },
            { 39, typeof(LED0Power) },
            { 40, typeof(LED1Power) },
            { 41, typeof(LED0PWMFrequency) },
            { 42, typeof(LED0PWMDutyCycle) },
            { 43, typeof(LED0PWMNumberPulses) },
            { 44, typeof(LED0IntervalOn) },
            { 45, typeof(LED0IntervalOff) },
            { 46, typeof(LED0IntervalNumberPulses) },
            { 47, typeof(LED0IntervalTail) },
            { 48, typeof(LED0IntervalNumberRepeats) },
            { 49, typeof(LED1PWMFrequency) },
            { 50, typeof(LED1PWMDutyCycle) },
            { 51, typeof(LED1PWMNumberPulses) },
            { 52, typeof(LED1IntervalOn) },
            { 53, typeof(LED1IntervalOff) },
            { 54, typeof(LED1IntervalNumberPulses) },
            { 55, typeof(LED1IntervalTail) },
            { 56, typeof(LED1IntervalNumberRepeats) },
            { 57, typeof(LED0PWMFrequencyReal) },
            { 58, typeof(LED0PWMDutyCycleReal) },
            { 59, typeof(LED1PWMFrequencyReal) },
            { 60, typeof(LED1PWMDutyCycleReal) },
            { 61, typeof(AuxDigitalOutputState) },
            { 62, typeof(AuxLEDPower) },
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
    /// <seealso cref="EnableBehavior"/>
    /// <seealso cref="EnableLED"/>
    /// <seealso cref="DigitalInputState"/>
    /// <seealso cref="DigitalOutputSync"/>
    /// <seealso cref="DigitalInputTrigger"/>
    /// <seealso cref="LEDMode"/>
    /// <seealso cref="LED0Power"/>
    /// <seealso cref="LED1Power"/>
    /// <seealso cref="LED0PWMFrequency"/>
    /// <seealso cref="LED0PWMDutyCycle"/>
    /// <seealso cref="LED0PWMNumberPulses"/>
    /// <seealso cref="LED0IntervalOn"/>
    /// <seealso cref="LED0IntervalOff"/>
    /// <seealso cref="LED0IntervalNumberPulses"/>
    /// <seealso cref="LED0IntervalTail"/>
    /// <seealso cref="LED0IntervalNumberRepeats"/>
    /// <seealso cref="LED1PWMFrequency"/>
    /// <seealso cref="LED1PWMDutyCycle"/>
    /// <seealso cref="LED1PWMNumberPulses"/>
    /// <seealso cref="LED1IntervalOn"/>
    /// <seealso cref="LED1IntervalOff"/>
    /// <seealso cref="LED1IntervalNumberPulses"/>
    /// <seealso cref="LED1IntervalTail"/>
    /// <seealso cref="LED1IntervalNumberRepeats"/>
    /// <seealso cref="LED0PWMFrequencyReal"/>
    /// <seealso cref="LED0PWMDutyCycleReal"/>
    /// <seealso cref="LED1PWMFrequencyReal"/>
    /// <seealso cref="LED1PWMDutyCycleReal"/>
    /// <seealso cref="AuxDigitalOutputState"/>
    /// <seealso cref="AuxLEDPower"/>
    /// <seealso cref="DigitalOutputState"/>
    /// <seealso cref="EnableEvents"/>
    [XmlInclude(typeof(EnablePower))]
    [XmlInclude(typeof(EnableBehavior))]
    [XmlInclude(typeof(EnableLED))]
    [XmlInclude(typeof(DigitalInputState))]
    [XmlInclude(typeof(DigitalOutputSync))]
    [XmlInclude(typeof(DigitalInputTrigger))]
    [XmlInclude(typeof(LEDMode))]
    [XmlInclude(typeof(LED0Power))]
    [XmlInclude(typeof(LED1Power))]
    [XmlInclude(typeof(LED0PWMFrequency))]
    [XmlInclude(typeof(LED0PWMDutyCycle))]
    [XmlInclude(typeof(LED0PWMNumberPulses))]
    [XmlInclude(typeof(LED0IntervalOn))]
    [XmlInclude(typeof(LED0IntervalOff))]
    [XmlInclude(typeof(LED0IntervalNumberPulses))]
    [XmlInclude(typeof(LED0IntervalTail))]
    [XmlInclude(typeof(LED0IntervalNumberRepeats))]
    [XmlInclude(typeof(LED1PWMFrequency))]
    [XmlInclude(typeof(LED1PWMDutyCycle))]
    [XmlInclude(typeof(LED1PWMNumberPulses))]
    [XmlInclude(typeof(LED1IntervalOn))]
    [XmlInclude(typeof(LED1IntervalOff))]
    [XmlInclude(typeof(LED1IntervalNumberPulses))]
    [XmlInclude(typeof(LED1IntervalTail))]
    [XmlInclude(typeof(LED1IntervalNumberRepeats))]
    [XmlInclude(typeof(LED0PWMFrequencyReal))]
    [XmlInclude(typeof(LED0PWMDutyCycleReal))]
    [XmlInclude(typeof(LED1PWMFrequencyReal))]
    [XmlInclude(typeof(LED1PWMDutyCycleReal))]
    [XmlInclude(typeof(AuxDigitalOutputState))]
    [XmlInclude(typeof(AuxLEDPower))]
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
    /// <seealso cref="EnableBehavior"/>
    /// <seealso cref="EnableLED"/>
    /// <seealso cref="DigitalInputState"/>
    /// <seealso cref="DigitalOutputSync"/>
    /// <seealso cref="DigitalInputTrigger"/>
    /// <seealso cref="LEDMode"/>
    /// <seealso cref="LED0Power"/>
    /// <seealso cref="LED1Power"/>
    /// <seealso cref="LED0PWMFrequency"/>
    /// <seealso cref="LED0PWMDutyCycle"/>
    /// <seealso cref="LED0PWMNumberPulses"/>
    /// <seealso cref="LED0IntervalOn"/>
    /// <seealso cref="LED0IntervalOff"/>
    /// <seealso cref="LED0IntervalNumberPulses"/>
    /// <seealso cref="LED0IntervalTail"/>
    /// <seealso cref="LED0IntervalNumberRepeats"/>
    /// <seealso cref="LED1PWMFrequency"/>
    /// <seealso cref="LED1PWMDutyCycle"/>
    /// <seealso cref="LED1PWMNumberPulses"/>
    /// <seealso cref="LED1IntervalOn"/>
    /// <seealso cref="LED1IntervalOff"/>
    /// <seealso cref="LED1IntervalNumberPulses"/>
    /// <seealso cref="LED1IntervalTail"/>
    /// <seealso cref="LED1IntervalNumberRepeats"/>
    /// <seealso cref="LED0PWMFrequencyReal"/>
    /// <seealso cref="LED0PWMDutyCycleReal"/>
    /// <seealso cref="LED1PWMFrequencyReal"/>
    /// <seealso cref="LED1PWMDutyCycleReal"/>
    /// <seealso cref="AuxDigitalOutputState"/>
    /// <seealso cref="AuxLEDPower"/>
    /// <seealso cref="DigitalOutputState"/>
    /// <seealso cref="EnableEvents"/>
    [XmlInclude(typeof(EnablePower))]
    [XmlInclude(typeof(EnableBehavior))]
    [XmlInclude(typeof(EnableLED))]
    [XmlInclude(typeof(DigitalInputState))]
    [XmlInclude(typeof(DigitalOutputSync))]
    [XmlInclude(typeof(DigitalInputTrigger))]
    [XmlInclude(typeof(LEDMode))]
    [XmlInclude(typeof(LED0Power))]
    [XmlInclude(typeof(LED1Power))]
    [XmlInclude(typeof(LED0PWMFrequency))]
    [XmlInclude(typeof(LED0PWMDutyCycle))]
    [XmlInclude(typeof(LED0PWMNumberPulses))]
    [XmlInclude(typeof(LED0IntervalOn))]
    [XmlInclude(typeof(LED0IntervalOff))]
    [XmlInclude(typeof(LED0IntervalNumberPulses))]
    [XmlInclude(typeof(LED0IntervalTail))]
    [XmlInclude(typeof(LED0IntervalNumberRepeats))]
    [XmlInclude(typeof(LED1PWMFrequency))]
    [XmlInclude(typeof(LED1PWMDutyCycle))]
    [XmlInclude(typeof(LED1PWMNumberPulses))]
    [XmlInclude(typeof(LED1IntervalOn))]
    [XmlInclude(typeof(LED1IntervalOff))]
    [XmlInclude(typeof(LED1IntervalNumberPulses))]
    [XmlInclude(typeof(LED1IntervalTail))]
    [XmlInclude(typeof(LED1IntervalNumberRepeats))]
    [XmlInclude(typeof(LED0PWMFrequencyReal))]
    [XmlInclude(typeof(LED0PWMDutyCycleReal))]
    [XmlInclude(typeof(LED1PWMFrequencyReal))]
    [XmlInclude(typeof(LED1PWMDutyCycleReal))]
    [XmlInclude(typeof(AuxDigitalOutputState))]
    [XmlInclude(typeof(AuxLEDPower))]
    [XmlInclude(typeof(DigitalOutputState))]
    [XmlInclude(typeof(EnableEvents))]
    [XmlInclude(typeof(TimestampedEnablePower))]
    [XmlInclude(typeof(TimestampedEnableBehavior))]
    [XmlInclude(typeof(TimestampedEnableLED))]
    [XmlInclude(typeof(TimestampedDigitalInputState))]
    [XmlInclude(typeof(TimestampedDigitalOutputSync))]
    [XmlInclude(typeof(TimestampedDigitalInputTrigger))]
    [XmlInclude(typeof(TimestampedLEDMode))]
    [XmlInclude(typeof(TimestampedLED0Power))]
    [XmlInclude(typeof(TimestampedLED1Power))]
    [XmlInclude(typeof(TimestampedLED0PWMFrequency))]
    [XmlInclude(typeof(TimestampedLED0PWMDutyCycle))]
    [XmlInclude(typeof(TimestampedLED0PWMNumberPulses))]
    [XmlInclude(typeof(TimestampedLED0IntervalOn))]
    [XmlInclude(typeof(TimestampedLED0IntervalOff))]
    [XmlInclude(typeof(TimestampedLED0IntervalNumberPulses))]
    [XmlInclude(typeof(TimestampedLED0IntervalTail))]
    [XmlInclude(typeof(TimestampedLED0IntervalNumberRepeats))]
    [XmlInclude(typeof(TimestampedLED1PWMFrequency))]
    [XmlInclude(typeof(TimestampedLED1PWMDutyCycle))]
    [XmlInclude(typeof(TimestampedLED1PWMNumberPulses))]
    [XmlInclude(typeof(TimestampedLED1IntervalOn))]
    [XmlInclude(typeof(TimestampedLED1IntervalOff))]
    [XmlInclude(typeof(TimestampedLED1IntervalNumberPulses))]
    [XmlInclude(typeof(TimestampedLED1IntervalTail))]
    [XmlInclude(typeof(TimestampedLED1IntervalNumberRepeats))]
    [XmlInclude(typeof(TimestampedLED0PWMFrequencyReal))]
    [XmlInclude(typeof(TimestampedLED0PWMDutyCycleReal))]
    [XmlInclude(typeof(TimestampedLED1PWMFrequencyReal))]
    [XmlInclude(typeof(TimestampedLED1PWMDutyCycleReal))]
    [XmlInclude(typeof(TimestampedAuxDigitalOutputState))]
    [XmlInclude(typeof(TimestampedAuxLEDPower))]
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
    /// <seealso cref="EnableBehavior"/>
    /// <seealso cref="EnableLED"/>
    /// <seealso cref="DigitalInputState"/>
    /// <seealso cref="DigitalOutputSync"/>
    /// <seealso cref="DigitalInputTrigger"/>
    /// <seealso cref="LEDMode"/>
    /// <seealso cref="LED0Power"/>
    /// <seealso cref="LED1Power"/>
    /// <seealso cref="LED0PWMFrequency"/>
    /// <seealso cref="LED0PWMDutyCycle"/>
    /// <seealso cref="LED0PWMNumberPulses"/>
    /// <seealso cref="LED0IntervalOn"/>
    /// <seealso cref="LED0IntervalOff"/>
    /// <seealso cref="LED0IntervalNumberPulses"/>
    /// <seealso cref="LED0IntervalTail"/>
    /// <seealso cref="LED0IntervalNumberRepeats"/>
    /// <seealso cref="LED1PWMFrequency"/>
    /// <seealso cref="LED1PWMDutyCycle"/>
    /// <seealso cref="LED1PWMNumberPulses"/>
    /// <seealso cref="LED1IntervalOn"/>
    /// <seealso cref="LED1IntervalOff"/>
    /// <seealso cref="LED1IntervalNumberPulses"/>
    /// <seealso cref="LED1IntervalTail"/>
    /// <seealso cref="LED1IntervalNumberRepeats"/>
    /// <seealso cref="LED0PWMFrequencyReal"/>
    /// <seealso cref="LED0PWMDutyCycleReal"/>
    /// <seealso cref="LED1PWMFrequencyReal"/>
    /// <seealso cref="LED1PWMDutyCycleReal"/>
    /// <seealso cref="AuxDigitalOutputState"/>
    /// <seealso cref="AuxLEDPower"/>
    /// <seealso cref="DigitalOutputState"/>
    /// <seealso cref="EnableEvents"/>
    [XmlInclude(typeof(EnablePower))]
    [XmlInclude(typeof(EnableBehavior))]
    [XmlInclude(typeof(EnableLED))]
    [XmlInclude(typeof(DigitalInputState))]
    [XmlInclude(typeof(DigitalOutputSync))]
    [XmlInclude(typeof(DigitalInputTrigger))]
    [XmlInclude(typeof(LEDMode))]
    [XmlInclude(typeof(LED0Power))]
    [XmlInclude(typeof(LED1Power))]
    [XmlInclude(typeof(LED0PWMFrequency))]
    [XmlInclude(typeof(LED0PWMDutyCycle))]
    [XmlInclude(typeof(LED0PWMNumberPulses))]
    [XmlInclude(typeof(LED0IntervalOn))]
    [XmlInclude(typeof(LED0IntervalOff))]
    [XmlInclude(typeof(LED0IntervalNumberPulses))]
    [XmlInclude(typeof(LED0IntervalTail))]
    [XmlInclude(typeof(LED0IntervalNumberRepeats))]
    [XmlInclude(typeof(LED1PWMFrequency))]
    [XmlInclude(typeof(LED1PWMDutyCycle))]
    [XmlInclude(typeof(LED1PWMNumberPulses))]
    [XmlInclude(typeof(LED1IntervalOn))]
    [XmlInclude(typeof(LED1IntervalOff))]
    [XmlInclude(typeof(LED1IntervalNumberPulses))]
    [XmlInclude(typeof(LED1IntervalTail))]
    [XmlInclude(typeof(LED1IntervalNumberRepeats))]
    [XmlInclude(typeof(LED0PWMFrequencyReal))]
    [XmlInclude(typeof(LED0PWMDutyCycleReal))]
    [XmlInclude(typeof(LED1PWMFrequencyReal))]
    [XmlInclude(typeof(LED1PWMDutyCycleReal))]
    [XmlInclude(typeof(AuxDigitalOutputState))]
    [XmlInclude(typeof(AuxLEDPower))]
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
        public static LEDs GetPayload(HarpMessage message)
        {
            return (LEDs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EnablePower"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LEDs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((LEDs)payload.Value, payload.Seconds);
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
        public static HarpMessage FromPayload(MessageType messageType, LEDs value)
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
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, LEDs value)
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
        public static Timestamped<LEDs> GetPayload(HarpMessage message)
        {
            return EnablePower.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that start/stop the LEDs according to the behavior pulse configuration.
    /// </summary>
    [Description("Start/stop the LEDs according to the behavior pulse configuration.")]
    public partial class EnableBehavior
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableBehavior"/> register. This field is constant.
        /// </summary>
        public const int Address = 33;

        /// <summary>
        /// Represents the payload type of the <see cref="EnableBehavior"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EnableBehavior"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EnableBehavior"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static LEDs GetPayload(HarpMessage message)
        {
            return (LEDs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EnableBehavior"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LEDs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((LEDs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EnableBehavior"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableBehavior"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, LEDs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EnableBehavior"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableBehavior"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, LEDs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EnableBehavior register.
    /// </summary>
    /// <seealso cref="EnableBehavior"/>
    [Description("Filters and selects timestamped messages from the EnableBehavior register.")]
    public partial class TimestampedEnableBehavior
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableBehavior"/> register. This field is constant.
        /// </summary>
        public const int Address = EnableBehavior.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EnableBehavior"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LEDs> GetPayload(HarpMessage message)
        {
            return EnableBehavior.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that enables the stimulation LEDs.
    /// </summary>
    [Description("Enables the stimulation LEDs.")]
    public partial class EnableLED
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableLED"/> register. This field is constant.
        /// </summary>
        public const int Address = 34;

        /// <summary>
        /// Represents the payload type of the <see cref="EnableLED"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EnableLED"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EnableLED"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static LEDs GetPayload(HarpMessage message)
        {
            return (LEDs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EnableLED"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LEDs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((LEDs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EnableLED"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableLED"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, LEDs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EnableLED"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableLED"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, LEDs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EnableLED register.
    /// </summary>
    /// <seealso cref="EnableLED"/>
    [Description("Filters and selects timestamped messages from the EnableLED register.")]
    public partial class TimestampedEnableLED
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableLED"/> register. This field is constant.
        /// </summary>
        public const int Address = EnableLED.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EnableLED"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LEDs> GetPayload(HarpMessage message)
        {
            return EnableLED.GetTimestampedPayload(message);
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
    /// Represents a register that configuration of the digital outputs functionality.
    /// </summary>
    [Description("Configuration of the digital outputs functionality.")]
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
    /// Represents a register that configuration of the digital inputs pins.
    /// </summary>
    [Description("Configuration of the digital inputs pins.")]
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
            result.DI0Trigger = (DI0TriggerConfig)(byte)(payload & 0x7);
            result.DI1Trigger = (DI1TriggerConfig)(byte)((payload & 0x70) >> 4);
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
    /// Represents a register that sets the configuration mode of the LED when behavior is enabled.
    /// </summary>
    [Description("Sets the configuration mode of the LED when behavior is enabled.")]
    public partial class LEDMode
    {
        /// <summary>
        /// Represents the address of the <see cref="LEDMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 38;

        /// <summary>
        /// Represents the payload type of the <see cref="LEDMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="LEDMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        static LEDModePayload ParsePayload(byte payload)
        {
            LEDModePayload result;
            result.LED0Mode = (LED0ModeConfig)(byte)(payload & 0x3);
            result.LED1Mode = (LED1ModeConfig)(byte)((payload & 0x30) >> 4);
            return result;
        }

        static byte FormatPayload(LEDModePayload value)
        {
            byte result;
            result = (byte)((byte)value.LED0Mode & 0x3);
            result |= (byte)(((byte)value.LED1Mode << 4) & 0x30);
            return result;
        }

        /// <summary>
        /// Returns the payload data for <see cref="LEDMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static LEDModePayload GetPayload(HarpMessage message)
        {
            return ParsePayload(message.GetPayloadByte());
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LEDMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LEDModePayload> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create(ParsePayload(payload.Value), payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LEDMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LEDMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, LEDModePayload value)
        {
            return HarpMessage.FromByte(Address, messageType, FormatPayload(value));
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LEDMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LEDMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, LEDModePayload value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, FormatPayload(value));
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LEDMode register.
    /// </summary>
    /// <seealso cref="LEDMode"/>
    [Description("Filters and selects timestamped messages from the LEDMode register.")]
    public partial class TimestampedLEDMode
    {
        /// <summary>
        /// Represents the address of the <see cref="LEDMode"/> register. This field is constant.
        /// </summary>
        public const int Address = LEDMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LEDMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LEDModePayload> GetPayload(HarpMessage message)
        {
            return LEDMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the power to be aplied to LED0, between 1 and 120.
    /// </summary>
    [Description("Sets the power to be aplied to LED0, between 1 and 120.")]
    public partial class LED0Power
    {
        /// <summary>
        /// Represents the address of the <see cref="LED0Power"/> register. This field is constant.
        /// </summary>
        public const int Address = 39;

        /// <summary>
        /// Represents the payload type of the <see cref="LED0Power"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="LED0Power"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LED0Power"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LED0Power"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LED0Power"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED0Power"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LED0Power"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED0Power"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LED0Power register.
    /// </summary>
    /// <seealso cref="LED0Power"/>
    [Description("Filters and selects timestamped messages from the LED0Power register.")]
    public partial class TimestampedLED0Power
    {
        /// <summary>
        /// Represents the address of the <see cref="LED0Power"/> register. This field is constant.
        /// </summary>
        public const int Address = LED0Power.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LED0Power"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return LED0Power.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the power to be aplied to LED1, between 1 and 120.
    /// </summary>
    [Description("Sets the power to be aplied to LED1, between 1 and 120.")]
    public partial class LED1Power
    {
        /// <summary>
        /// Represents the address of the <see cref="LED1Power"/> register. This field is constant.
        /// </summary>
        public const int Address = 40;

        /// <summary>
        /// Represents the payload type of the <see cref="LED1Power"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="LED1Power"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LED1Power"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LED1Power"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LED1Power"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED1Power"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LED1Power"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED1Power"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LED1Power register.
    /// </summary>
    /// <seealso cref="LED1Power"/>
    [Description("Filters and selects timestamped messages from the LED1Power register.")]
    public partial class TimestampedLED1Power
    {
        /// <summary>
        /// Represents the address of the <see cref="LED1Power"/> register. This field is constant.
        /// </summary>
        public const int Address = LED1Power.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LED1Power"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return LED1Power.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the	PWM frequency (Hz) of LED0's when in PWM mode, between 0.5 and 2000.
    /// </summary>
    [Description("Sets the	PWM frequency (Hz) of LED0's when in PWM mode, between 0.5 and 2000.")]
    public partial class LED0PWMFrequency
    {
        /// <summary>
        /// Represents the address of the <see cref="LED0PWMFrequency"/> register. This field is constant.
        /// </summary>
        public const int Address = 41;

        /// <summary>
        /// Represents the payload type of the <see cref="LED0PWMFrequency"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="LED0PWMFrequency"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LED0PWMFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LED0PWMFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LED0PWMFrequency"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED0PWMFrequency"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LED0PWMFrequency"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED0PWMFrequency"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LED0PWMFrequency register.
    /// </summary>
    /// <seealso cref="LED0PWMFrequency"/>
    [Description("Filters and selects timestamped messages from the LED0PWMFrequency register.")]
    public partial class TimestampedLED0PWMFrequency
    {
        /// <summary>
        /// Represents the address of the <see cref="LED0PWMFrequency"/> register. This field is constant.
        /// </summary>
        public const int Address = LED0PWMFrequency.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LED0PWMFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return LED0PWMFrequency.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the	PWM duty cycle (%) of LED0's when in PWM mode, between 0.1 and 99.9.
    /// </summary>
    [Description("Sets the	PWM duty cycle (%) of LED0's when in PWM mode, between 0.1 and 99.9.")]
    public partial class LED0PWMDutyCycle
    {
        /// <summary>
        /// Represents the address of the <see cref="LED0PWMDutyCycle"/> register. This field is constant.
        /// </summary>
        public const int Address = 42;

        /// <summary>
        /// Represents the payload type of the <see cref="LED0PWMDutyCycle"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="LED0PWMDutyCycle"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LED0PWMDutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LED0PWMDutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LED0PWMDutyCycle"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED0PWMDutyCycle"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LED0PWMDutyCycle"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED0PWMDutyCycle"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LED0PWMDutyCycle register.
    /// </summary>
    /// <seealso cref="LED0PWMDutyCycle"/>
    [Description("Filters and selects timestamped messages from the LED0PWMDutyCycle register.")]
    public partial class TimestampedLED0PWMDutyCycle
    {
        /// <summary>
        /// Represents the address of the <see cref="LED0PWMDutyCycle"/> register. This field is constant.
        /// </summary>
        public const int Address = LED0PWMDutyCycle.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LED0PWMDutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return LED0PWMDutyCycle.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the	PWM number of pulses of LED0's when in PWM mode, between 1 and 65535.
    /// </summary>
    [Description("Sets the	PWM number of pulses of LED0's when in PWM mode, between 1 and 65535.")]
    public partial class LED0PWMNumberPulses
    {
        /// <summary>
        /// Represents the address of the <see cref="LED0PWMNumberPulses"/> register. This field is constant.
        /// </summary>
        public const int Address = 43;

        /// <summary>
        /// Represents the payload type of the <see cref="LED0PWMNumberPulses"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="LED0PWMNumberPulses"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LED0PWMNumberPulses"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LED0PWMNumberPulses"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LED0PWMNumberPulses"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED0PWMNumberPulses"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LED0PWMNumberPulses"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED0PWMNumberPulses"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LED0PWMNumberPulses register.
    /// </summary>
    /// <seealso cref="LED0PWMNumberPulses"/>
    [Description("Filters and selects timestamped messages from the LED0PWMNumberPulses register.")]
    public partial class TimestampedLED0PWMNumberPulses
    {
        /// <summary>
        /// Represents the address of the <see cref="LED0PWMNumberPulses"/> register. This field is constant.
        /// </summary>
        public const int Address = LED0PWMNumberPulses.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LED0PWMNumberPulses"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return LED0PWMNumberPulses.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the	time on (milliseconds) of LED0's when in interval mode, between 1 and 65535.
    /// </summary>
    [Description("Sets the	time on (milliseconds) of LED0's when in interval mode, between 1 and 65535.")]
    public partial class LED0IntervalOn
    {
        /// <summary>
        /// Represents the address of the <see cref="LED0IntervalOn"/> register. This field is constant.
        /// </summary>
        public const int Address = 44;

        /// <summary>
        /// Represents the payload type of the <see cref="LED0IntervalOn"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="LED0IntervalOn"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LED0IntervalOn"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LED0IntervalOn"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LED0IntervalOn"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED0IntervalOn"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LED0IntervalOn"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED0IntervalOn"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LED0IntervalOn register.
    /// </summary>
    /// <seealso cref="LED0IntervalOn"/>
    [Description("Filters and selects timestamped messages from the LED0IntervalOn register.")]
    public partial class TimestampedLED0IntervalOn
    {
        /// <summary>
        /// Represents the address of the <see cref="LED0IntervalOn"/> register. This field is constant.
        /// </summary>
        public const int Address = LED0IntervalOn.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LED0IntervalOn"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return LED0IntervalOn.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the	time off (milliseconds) of LED0's when in interval mode, between 1 and 65535.
    /// </summary>
    [Description("Sets the	time off (milliseconds) of LED0's when in interval mode, between 1 and 65535.")]
    public partial class LED0IntervalOff
    {
        /// <summary>
        /// Represents the address of the <see cref="LED0IntervalOff"/> register. This field is constant.
        /// </summary>
        public const int Address = 45;

        /// <summary>
        /// Represents the payload type of the <see cref="LED0IntervalOff"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="LED0IntervalOff"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LED0IntervalOff"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LED0IntervalOff"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LED0IntervalOff"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED0IntervalOff"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LED0IntervalOff"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED0IntervalOff"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LED0IntervalOff register.
    /// </summary>
    /// <seealso cref="LED0IntervalOff"/>
    [Description("Filters and selects timestamped messages from the LED0IntervalOff register.")]
    public partial class TimestampedLED0IntervalOff
    {
        /// <summary>
        /// Represents the address of the <see cref="LED0IntervalOff"/> register. This field is constant.
        /// </summary>
        public const int Address = LED0IntervalOff.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LED0IntervalOff"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return LED0IntervalOff.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the	number of pulses of LED0's when in interval mode, between 1 and 65535.
    /// </summary>
    [Description("Sets the	number of pulses of LED0's when in interval mode, between 1 and 65535.")]
    public partial class LED0IntervalNumberPulses
    {
        /// <summary>
        /// Represents the address of the <see cref="LED0IntervalNumberPulses"/> register. This field is constant.
        /// </summary>
        public const int Address = 46;

        /// <summary>
        /// Represents the payload type of the <see cref="LED0IntervalNumberPulses"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="LED0IntervalNumberPulses"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LED0IntervalNumberPulses"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LED0IntervalNumberPulses"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LED0IntervalNumberPulses"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED0IntervalNumberPulses"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LED0IntervalNumberPulses"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED0IntervalNumberPulses"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LED0IntervalNumberPulses register.
    /// </summary>
    /// <seealso cref="LED0IntervalNumberPulses"/>
    [Description("Filters and selects timestamped messages from the LED0IntervalNumberPulses register.")]
    public partial class TimestampedLED0IntervalNumberPulses
    {
        /// <summary>
        /// Represents the address of the <see cref="LED0IntervalNumberPulses"/> register. This field is constant.
        /// </summary>
        public const int Address = LED0IntervalNumberPulses.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LED0IntervalNumberPulses"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return LED0IntervalNumberPulses.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the	wait time between pulses (milliseconds) of LED0's when in interval mode, between 1 and 65535.
    /// </summary>
    [Description("Sets the	wait time between pulses (milliseconds) of LED0's when in interval mode, between 1 and 65535.")]
    public partial class LED0IntervalTail
    {
        /// <summary>
        /// Represents the address of the <see cref="LED0IntervalTail"/> register. This field is constant.
        /// </summary>
        public const int Address = 47;

        /// <summary>
        /// Represents the payload type of the <see cref="LED0IntervalTail"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="LED0IntervalTail"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LED0IntervalTail"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LED0IntervalTail"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LED0IntervalTail"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED0IntervalTail"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LED0IntervalTail"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED0IntervalTail"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LED0IntervalTail register.
    /// </summary>
    /// <seealso cref="LED0IntervalTail"/>
    [Description("Filters and selects timestamped messages from the LED0IntervalTail register.")]
    public partial class TimestampedLED0IntervalTail
    {
        /// <summary>
        /// Represents the address of the <see cref="LED0IntervalTail"/> register. This field is constant.
        /// </summary>
        public const int Address = LED0IntervalTail.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LED0IntervalTail"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return LED0IntervalTail.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the	number of repetitions of LED0's pulse when in interval mode, between 1 and 65535.
    /// </summary>
    [Description("Sets the	number of repetitions of LED0's pulse when in interval mode, between 1 and 65535.")]
    public partial class LED0IntervalNumberRepeats
    {
        /// <summary>
        /// Represents the address of the <see cref="LED0IntervalNumberRepeats"/> register. This field is constant.
        /// </summary>
        public const int Address = 48;

        /// <summary>
        /// Represents the payload type of the <see cref="LED0IntervalNumberRepeats"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="LED0IntervalNumberRepeats"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LED0IntervalNumberRepeats"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LED0IntervalNumberRepeats"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LED0IntervalNumberRepeats"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED0IntervalNumberRepeats"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LED0IntervalNumberRepeats"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED0IntervalNumberRepeats"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LED0IntervalNumberRepeats register.
    /// </summary>
    /// <seealso cref="LED0IntervalNumberRepeats"/>
    [Description("Filters and selects timestamped messages from the LED0IntervalNumberRepeats register.")]
    public partial class TimestampedLED0IntervalNumberRepeats
    {
        /// <summary>
        /// Represents the address of the <see cref="LED0IntervalNumberRepeats"/> register. This field is constant.
        /// </summary>
        public const int Address = LED0IntervalNumberRepeats.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LED0IntervalNumberRepeats"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return LED0IntervalNumberRepeats.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the	PWM frequency (Hz) of LED1's when in PWM mode, between 0.5 and 2000.
    /// </summary>
    [Description("Sets the	PWM frequency (Hz) of LED1's when in PWM mode, between 0.5 and 2000.")]
    public partial class LED1PWMFrequency
    {
        /// <summary>
        /// Represents the address of the <see cref="LED1PWMFrequency"/> register. This field is constant.
        /// </summary>
        public const int Address = 49;

        /// <summary>
        /// Represents the payload type of the <see cref="LED1PWMFrequency"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="LED1PWMFrequency"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LED1PWMFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LED1PWMFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LED1PWMFrequency"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED1PWMFrequency"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LED1PWMFrequency"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED1PWMFrequency"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LED1PWMFrequency register.
    /// </summary>
    /// <seealso cref="LED1PWMFrequency"/>
    [Description("Filters and selects timestamped messages from the LED1PWMFrequency register.")]
    public partial class TimestampedLED1PWMFrequency
    {
        /// <summary>
        /// Represents the address of the <see cref="LED1PWMFrequency"/> register. This field is constant.
        /// </summary>
        public const int Address = LED1PWMFrequency.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LED1PWMFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return LED1PWMFrequency.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the	PWM duty cycle (%) of LED1's when in PWM mode, between 0.1 and 99.9.
    /// </summary>
    [Description("Sets the	PWM duty cycle (%) of LED1's when in PWM mode, between 0.1 and 99.9.")]
    public partial class LED1PWMDutyCycle
    {
        /// <summary>
        /// Represents the address of the <see cref="LED1PWMDutyCycle"/> register. This field is constant.
        /// </summary>
        public const int Address = 50;

        /// <summary>
        /// Represents the payload type of the <see cref="LED1PWMDutyCycle"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="LED1PWMDutyCycle"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LED1PWMDutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LED1PWMDutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LED1PWMDutyCycle"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED1PWMDutyCycle"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LED1PWMDutyCycle"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED1PWMDutyCycle"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LED1PWMDutyCycle register.
    /// </summary>
    /// <seealso cref="LED1PWMDutyCycle"/>
    [Description("Filters and selects timestamped messages from the LED1PWMDutyCycle register.")]
    public partial class TimestampedLED1PWMDutyCycle
    {
        /// <summary>
        /// Represents the address of the <see cref="LED1PWMDutyCycle"/> register. This field is constant.
        /// </summary>
        public const int Address = LED1PWMDutyCycle.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LED1PWMDutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return LED1PWMDutyCycle.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the	PWM number of pulses of LED1's when in PWM mode, between 1 and 65535.
    /// </summary>
    [Description("Sets the	PWM number of pulses of LED1's when in PWM mode, between 1 and 65535.")]
    public partial class LED1PWMNumberPulses
    {
        /// <summary>
        /// Represents the address of the <see cref="LED1PWMNumberPulses"/> register. This field is constant.
        /// </summary>
        public const int Address = 51;

        /// <summary>
        /// Represents the payload type of the <see cref="LED1PWMNumberPulses"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="LED1PWMNumberPulses"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LED1PWMNumberPulses"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LED1PWMNumberPulses"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LED1PWMNumberPulses"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED1PWMNumberPulses"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LED1PWMNumberPulses"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED1PWMNumberPulses"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LED1PWMNumberPulses register.
    /// </summary>
    /// <seealso cref="LED1PWMNumberPulses"/>
    [Description("Filters and selects timestamped messages from the LED1PWMNumberPulses register.")]
    public partial class TimestampedLED1PWMNumberPulses
    {
        /// <summary>
        /// Represents the address of the <see cref="LED1PWMNumberPulses"/> register. This field is constant.
        /// </summary>
        public const int Address = LED1PWMNumberPulses.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LED1PWMNumberPulses"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return LED1PWMNumberPulses.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the	time on (milliseconds) of LED1's when in interval mode, between 1 and 65535.
    /// </summary>
    [Description("Sets the	time on (milliseconds) of LED1's when in interval mode, between 1 and 65535.")]
    public partial class LED1IntervalOn
    {
        /// <summary>
        /// Represents the address of the <see cref="LED1IntervalOn"/> register. This field is constant.
        /// </summary>
        public const int Address = 52;

        /// <summary>
        /// Represents the payload type of the <see cref="LED1IntervalOn"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="LED1IntervalOn"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LED1IntervalOn"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LED1IntervalOn"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LED1IntervalOn"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED1IntervalOn"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LED1IntervalOn"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED1IntervalOn"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LED1IntervalOn register.
    /// </summary>
    /// <seealso cref="LED1IntervalOn"/>
    [Description("Filters and selects timestamped messages from the LED1IntervalOn register.")]
    public partial class TimestampedLED1IntervalOn
    {
        /// <summary>
        /// Represents the address of the <see cref="LED1IntervalOn"/> register. This field is constant.
        /// </summary>
        public const int Address = LED1IntervalOn.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LED1IntervalOn"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return LED1IntervalOn.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the	time off (milliseconds) of LED1's when in interval mode, between 1 and 65535.
    /// </summary>
    [Description("Sets the	time off (milliseconds) of LED1's when in interval mode, between 1 and 65535.")]
    public partial class LED1IntervalOff
    {
        /// <summary>
        /// Represents the address of the <see cref="LED1IntervalOff"/> register. This field is constant.
        /// </summary>
        public const int Address = 53;

        /// <summary>
        /// Represents the payload type of the <see cref="LED1IntervalOff"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="LED1IntervalOff"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LED1IntervalOff"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LED1IntervalOff"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LED1IntervalOff"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED1IntervalOff"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LED1IntervalOff"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED1IntervalOff"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LED1IntervalOff register.
    /// </summary>
    /// <seealso cref="LED1IntervalOff"/>
    [Description("Filters and selects timestamped messages from the LED1IntervalOff register.")]
    public partial class TimestampedLED1IntervalOff
    {
        /// <summary>
        /// Represents the address of the <see cref="LED1IntervalOff"/> register. This field is constant.
        /// </summary>
        public const int Address = LED1IntervalOff.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LED1IntervalOff"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return LED1IntervalOff.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the	number of pulses of LED1's when in interval mode, between 1 and 65535.
    /// </summary>
    [Description("Sets the	number of pulses of LED1's when in interval mode, between 1 and 65535.")]
    public partial class LED1IntervalNumberPulses
    {
        /// <summary>
        /// Represents the address of the <see cref="LED1IntervalNumberPulses"/> register. This field is constant.
        /// </summary>
        public const int Address = 54;

        /// <summary>
        /// Represents the payload type of the <see cref="LED1IntervalNumberPulses"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="LED1IntervalNumberPulses"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LED1IntervalNumberPulses"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LED1IntervalNumberPulses"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LED1IntervalNumberPulses"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED1IntervalNumberPulses"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LED1IntervalNumberPulses"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED1IntervalNumberPulses"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LED1IntervalNumberPulses register.
    /// </summary>
    /// <seealso cref="LED1IntervalNumberPulses"/>
    [Description("Filters and selects timestamped messages from the LED1IntervalNumberPulses register.")]
    public partial class TimestampedLED1IntervalNumberPulses
    {
        /// <summary>
        /// Represents the address of the <see cref="LED1IntervalNumberPulses"/> register. This field is constant.
        /// </summary>
        public const int Address = LED1IntervalNumberPulses.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LED1IntervalNumberPulses"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return LED1IntervalNumberPulses.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the	wait time between pulses (milliseconds) of LED1's when in interval mode, between 1 and 65535.
    /// </summary>
    [Description("Sets the	wait time between pulses (milliseconds) of LED1's when in interval mode, between 1 and 65535.")]
    public partial class LED1IntervalTail
    {
        /// <summary>
        /// Represents the address of the <see cref="LED1IntervalTail"/> register. This field is constant.
        /// </summary>
        public const int Address = 55;

        /// <summary>
        /// Represents the payload type of the <see cref="LED1IntervalTail"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="LED1IntervalTail"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LED1IntervalTail"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LED1IntervalTail"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LED1IntervalTail"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED1IntervalTail"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LED1IntervalTail"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED1IntervalTail"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LED1IntervalTail register.
    /// </summary>
    /// <seealso cref="LED1IntervalTail"/>
    [Description("Filters and selects timestamped messages from the LED1IntervalTail register.")]
    public partial class TimestampedLED1IntervalTail
    {
        /// <summary>
        /// Represents the address of the <see cref="LED1IntervalTail"/> register. This field is constant.
        /// </summary>
        public const int Address = LED1IntervalTail.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LED1IntervalTail"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return LED1IntervalTail.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the	number of repetitions of LED1's pulse when in interval mode, between 1 and 65535.
    /// </summary>
    [Description("Sets the	number of repetitions of LED1's pulse when in interval mode, between 1 and 65535.")]
    public partial class LED1IntervalNumberRepeats
    {
        /// <summary>
        /// Represents the address of the <see cref="LED1IntervalNumberRepeats"/> register. This field is constant.
        /// </summary>
        public const int Address = 56;

        /// <summary>
        /// Represents the payload type of the <see cref="LED1IntervalNumberRepeats"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="LED1IntervalNumberRepeats"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LED1IntervalNumberRepeats"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LED1IntervalNumberRepeats"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LED1IntervalNumberRepeats"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED1IntervalNumberRepeats"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LED1IntervalNumberRepeats"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED1IntervalNumberRepeats"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LED1IntervalNumberRepeats register.
    /// </summary>
    /// <seealso cref="LED1IntervalNumberRepeats"/>
    [Description("Filters and selects timestamped messages from the LED1IntervalNumberRepeats register.")]
    public partial class TimestampedLED1IntervalNumberRepeats
    {
        /// <summary>
        /// Represents the address of the <see cref="LED1IntervalNumberRepeats"/> register. This field is constant.
        /// </summary>
        public const int Address = LED1IntervalNumberRepeats.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LED1IntervalNumberRepeats"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return LED1IntervalNumberRepeats.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that get the real	PWM frequency (Hz) of LED0's when in PWM mode.
    /// </summary>
    [Description("Get the real	PWM frequency (Hz) of LED0's when in PWM mode.")]
    public partial class LED0PWMFrequencyReal
    {
        /// <summary>
        /// Represents the address of the <see cref="LED0PWMFrequencyReal"/> register. This field is constant.
        /// </summary>
        public const int Address = 57;

        /// <summary>
        /// Represents the payload type of the <see cref="LED0PWMFrequencyReal"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="LED0PWMFrequencyReal"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LED0PWMFrequencyReal"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LED0PWMFrequencyReal"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LED0PWMFrequencyReal"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED0PWMFrequencyReal"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LED0PWMFrequencyReal"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED0PWMFrequencyReal"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LED0PWMFrequencyReal register.
    /// </summary>
    /// <seealso cref="LED0PWMFrequencyReal"/>
    [Description("Filters and selects timestamped messages from the LED0PWMFrequencyReal register.")]
    public partial class TimestampedLED0PWMFrequencyReal
    {
        /// <summary>
        /// Represents the address of the <see cref="LED0PWMFrequencyReal"/> register. This field is constant.
        /// </summary>
        public const int Address = LED0PWMFrequencyReal.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LED0PWMFrequencyReal"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return LED0PWMFrequencyReal.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that get the real	duty cycle (%) of LED0's when in PWM mode.
    /// </summary>
    [Description("Get the real	duty cycle (%) of LED0's when in PWM mode.")]
    public partial class LED0PWMDutyCycleReal
    {
        /// <summary>
        /// Represents the address of the <see cref="LED0PWMDutyCycleReal"/> register. This field is constant.
        /// </summary>
        public const int Address = 58;

        /// <summary>
        /// Represents the payload type of the <see cref="LED0PWMDutyCycleReal"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="LED0PWMDutyCycleReal"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LED0PWMDutyCycleReal"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LED0PWMDutyCycleReal"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LED0PWMDutyCycleReal"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED0PWMDutyCycleReal"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LED0PWMDutyCycleReal"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED0PWMDutyCycleReal"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LED0PWMDutyCycleReal register.
    /// </summary>
    /// <seealso cref="LED0PWMDutyCycleReal"/>
    [Description("Filters and selects timestamped messages from the LED0PWMDutyCycleReal register.")]
    public partial class TimestampedLED0PWMDutyCycleReal
    {
        /// <summary>
        /// Represents the address of the <see cref="LED0PWMDutyCycleReal"/> register. This field is constant.
        /// </summary>
        public const int Address = LED0PWMDutyCycleReal.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LED0PWMDutyCycleReal"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return LED0PWMDutyCycleReal.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that get the real	PWM frequency (Hz) of LED1's when in PWM mode.
    /// </summary>
    [Description("Get the real	PWM frequency (Hz) of LED1's when in PWM mode.")]
    public partial class LED1PWMFrequencyReal
    {
        /// <summary>
        /// Represents the address of the <see cref="LED1PWMFrequencyReal"/> register. This field is constant.
        /// </summary>
        public const int Address = 59;

        /// <summary>
        /// Represents the payload type of the <see cref="LED1PWMFrequencyReal"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="LED1PWMFrequencyReal"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LED1PWMFrequencyReal"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LED1PWMFrequencyReal"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LED1PWMFrequencyReal"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED1PWMFrequencyReal"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LED1PWMFrequencyReal"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED1PWMFrequencyReal"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LED1PWMFrequencyReal register.
    /// </summary>
    /// <seealso cref="LED1PWMFrequencyReal"/>
    [Description("Filters and selects timestamped messages from the LED1PWMFrequencyReal register.")]
    public partial class TimestampedLED1PWMFrequencyReal
    {
        /// <summary>
        /// Represents the address of the <see cref="LED1PWMFrequencyReal"/> register. This field is constant.
        /// </summary>
        public const int Address = LED1PWMFrequencyReal.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LED1PWMFrequencyReal"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return LED1PWMFrequencyReal.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that get the real	duty cycle (%) of LED1's when in PWM mode.
    /// </summary>
    [Description("Get the real	duty cycle (%) of LED1's when in PWM mode.")]
    public partial class LED1PWMDutyCycleReal
    {
        /// <summary>
        /// Represents the address of the <see cref="LED1PWMDutyCycleReal"/> register. This field is constant.
        /// </summary>
        public const int Address = 60;

        /// <summary>
        /// Represents the payload type of the <see cref="LED1PWMDutyCycleReal"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="LED1PWMDutyCycleReal"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LED1PWMDutyCycleReal"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LED1PWMDutyCycleReal"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LED1PWMDutyCycleReal"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED1PWMDutyCycleReal"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LED1PWMDutyCycleReal"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LED1PWMDutyCycleReal"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LED1PWMDutyCycleReal register.
    /// </summary>
    /// <seealso cref="LED1PWMDutyCycleReal"/>
    [Description("Filters and selects timestamped messages from the LED1PWMDutyCycleReal register.")]
    public partial class TimestampedLED1PWMDutyCycleReal
    {
        /// <summary>
        /// Represents the address of the <see cref="LED1PWMDutyCycleReal"/> register. This field is constant.
        /// </summary>
        public const int Address = LED1PWMDutyCycleReal.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LED1PWMDutyCycleReal"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return LED1PWMDutyCycleReal.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that write the state of the auxiliar digital output bit.
    /// </summary>
    [Description("Write the state of the auxiliar digital output bit.")]
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
    /// Represents a register that sets the power to be aplied to auxiliary LED, between 1 and 120.
    /// </summary>
    [Description("Sets the power to be aplied to auxiliary LED, between 1 and 120.")]
    public partial class AuxLEDPower
    {
        /// <summary>
        /// Represents the address of the <see cref="AuxLEDPower"/> register. This field is constant.
        /// </summary>
        public const int Address = 62;

        /// <summary>
        /// Represents the payload type of the <see cref="AuxLEDPower"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="AuxLEDPower"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="AuxLEDPower"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="AuxLEDPower"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="AuxLEDPower"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AuxLEDPower"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="AuxLEDPower"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AuxLEDPower"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// AuxLEDPower register.
    /// </summary>
    /// <seealso cref="AuxLEDPower"/>
    [Description("Filters and selects timestamped messages from the AuxLEDPower register.")]
    public partial class TimestampedAuxLEDPower
    {
        /// <summary>
        /// Represents the address of the <see cref="AuxLEDPower"/> register. This field is constant.
        /// </summary>
        public const int Address = AuxLEDPower.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="AuxLEDPower"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return AuxLEDPower.GetTimestampedPayload(message);
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
    /// <seealso cref="CreateEnableBehaviorPayload"/>
    /// <seealso cref="CreateEnableLEDPayload"/>
    /// <seealso cref="CreateDigitalInputStatePayload"/>
    /// <seealso cref="CreateDigitalOutputSyncPayload"/>
    /// <seealso cref="CreateDigitalInputTriggerPayload"/>
    /// <seealso cref="CreateLEDModePayload"/>
    /// <seealso cref="CreateLED0PowerPayload"/>
    /// <seealso cref="CreateLED1PowerPayload"/>
    /// <seealso cref="CreateLED0PWMFrequencyPayload"/>
    /// <seealso cref="CreateLED0PWMDutyCyclePayload"/>
    /// <seealso cref="CreateLED0PWMNumberPulsesPayload"/>
    /// <seealso cref="CreateLED0IntervalOnPayload"/>
    /// <seealso cref="CreateLED0IntervalOffPayload"/>
    /// <seealso cref="CreateLED0IntervalNumberPulsesPayload"/>
    /// <seealso cref="CreateLED0IntervalTailPayload"/>
    /// <seealso cref="CreateLED0IntervalNumberRepeatsPayload"/>
    /// <seealso cref="CreateLED1PWMFrequencyPayload"/>
    /// <seealso cref="CreateLED1PWMDutyCyclePayload"/>
    /// <seealso cref="CreateLED1PWMNumberPulsesPayload"/>
    /// <seealso cref="CreateLED1IntervalOnPayload"/>
    /// <seealso cref="CreateLED1IntervalOffPayload"/>
    /// <seealso cref="CreateLED1IntervalNumberPulsesPayload"/>
    /// <seealso cref="CreateLED1IntervalTailPayload"/>
    /// <seealso cref="CreateLED1IntervalNumberRepeatsPayload"/>
    /// <seealso cref="CreateLED0PWMFrequencyRealPayload"/>
    /// <seealso cref="CreateLED0PWMDutyCycleRealPayload"/>
    /// <seealso cref="CreateLED1PWMFrequencyRealPayload"/>
    /// <seealso cref="CreateLED1PWMDutyCycleRealPayload"/>
    /// <seealso cref="CreateAuxDigitalOutputStatePayload"/>
    /// <seealso cref="CreateAuxLEDPowerPayload"/>
    /// <seealso cref="CreateDigitalOutputStatePayload"/>
    /// <seealso cref="CreateEnableEventsPayload"/>
    [XmlInclude(typeof(CreateEnablePowerPayload))]
    [XmlInclude(typeof(CreateEnableBehaviorPayload))]
    [XmlInclude(typeof(CreateEnableLEDPayload))]
    [XmlInclude(typeof(CreateDigitalInputStatePayload))]
    [XmlInclude(typeof(CreateDigitalOutputSyncPayload))]
    [XmlInclude(typeof(CreateDigitalInputTriggerPayload))]
    [XmlInclude(typeof(CreateLEDModePayload))]
    [XmlInclude(typeof(CreateLED0PowerPayload))]
    [XmlInclude(typeof(CreateLED1PowerPayload))]
    [XmlInclude(typeof(CreateLED0PWMFrequencyPayload))]
    [XmlInclude(typeof(CreateLED0PWMDutyCyclePayload))]
    [XmlInclude(typeof(CreateLED0PWMNumberPulsesPayload))]
    [XmlInclude(typeof(CreateLED0IntervalOnPayload))]
    [XmlInclude(typeof(CreateLED0IntervalOffPayload))]
    [XmlInclude(typeof(CreateLED0IntervalNumberPulsesPayload))]
    [XmlInclude(typeof(CreateLED0IntervalTailPayload))]
    [XmlInclude(typeof(CreateLED0IntervalNumberRepeatsPayload))]
    [XmlInclude(typeof(CreateLED1PWMFrequencyPayload))]
    [XmlInclude(typeof(CreateLED1PWMDutyCyclePayload))]
    [XmlInclude(typeof(CreateLED1PWMNumberPulsesPayload))]
    [XmlInclude(typeof(CreateLED1IntervalOnPayload))]
    [XmlInclude(typeof(CreateLED1IntervalOffPayload))]
    [XmlInclude(typeof(CreateLED1IntervalNumberPulsesPayload))]
    [XmlInclude(typeof(CreateLED1IntervalTailPayload))]
    [XmlInclude(typeof(CreateLED1IntervalNumberRepeatsPayload))]
    [XmlInclude(typeof(CreateLED0PWMFrequencyRealPayload))]
    [XmlInclude(typeof(CreateLED0PWMDutyCycleRealPayload))]
    [XmlInclude(typeof(CreateLED1PWMFrequencyRealPayload))]
    [XmlInclude(typeof(CreateLED1PWMDutyCycleRealPayload))]
    [XmlInclude(typeof(CreateAuxDigitalOutputStatePayload))]
    [XmlInclude(typeof(CreateAuxLEDPowerPayload))]
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
        public LEDs Value { get; set; }

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
    /// that start/stop the LEDs according to the behavior pulse configuration.
    /// </summary>
    [DisplayName("EnableBehaviorPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that start/stop the LEDs according to the behavior pulse configuration.")]
    public partial class CreateEnableBehaviorPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that start/stop the LEDs according to the behavior pulse configuration.
        /// </summary>
        [Description("The value that start/stop the LEDs according to the behavior pulse configuration.")]
        public LEDs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that start/stop the LEDs according to the behavior pulse configuration.
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
        /// that start/stop the LEDs according to the behavior pulse configuration.
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
            return source.Select(_ => EnableBehavior.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that enables the stimulation LEDs.
    /// </summary>
    [DisplayName("EnableLEDPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that enables the stimulation LEDs.")]
    public partial class CreateEnableLEDPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that enables the stimulation LEDs.
        /// </summary>
        [Description("The value that enables the stimulation LEDs.")]
        public LEDs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that enables the stimulation LEDs.
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
        /// that enables the stimulation LEDs.
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
            return source.Select(_ => EnableLED.FromPayload(MessageType, Value));
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
    /// that configuration of the digital outputs functionality.
    /// </summary>
    [DisplayName("DigitalOutputSyncPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that configuration of the digital outputs functionality.")]
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
        /// that configuration of the digital outputs functionality.
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
        /// that configuration of the digital outputs functionality.
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
    /// that configuration of the digital inputs pins.
    /// </summary>
    [DisplayName("DigitalInputTriggerPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that configuration of the digital inputs pins.")]
    public partial class CreateDigitalInputTriggerPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets a value that configuration of the DIO input pin.
        /// </summary>
        [Description("Configuration of the DIO input pin.")]
        public DI0TriggerConfig DI0Trigger { get; set; }

        /// <summary>
        /// Gets or sets a value that configuration of the DI1 input pin.
        /// </summary>
        [Description("Configuration of the DI1 input pin.")]
        public DI1TriggerConfig DI1Trigger { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that configuration of the digital inputs pins.
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
        /// that configuration of the digital inputs pins.
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
    /// that sets the configuration mode of the LED when behavior is enabled.
    /// </summary>
    [DisplayName("LEDModePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the configuration mode of the LED when behavior is enabled.")]
    public partial class CreateLEDModePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets a value that sets the configuration mode of the LED0 when behavior is enabled.
        /// </summary>
        [Description("Sets the configuration mode of the LED0 when behavior is enabled.")]
        public LED0ModeConfig LED0Mode { get; set; }

        /// <summary>
        /// Gets or sets a value that sets the configuration mode of the LED1 when behavior is enabled.
        /// </summary>
        [Description("Sets the configuration mode of the LED1 when behavior is enabled.")]
        public LED1ModeConfig LED1Mode { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the configuration mode of the LED when behavior is enabled.
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
        /// that sets the configuration mode of the LED when behavior is enabled.
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
                LEDModePayload value;
                value.LED0Mode = LED0Mode;
                value.LED1Mode = LED1Mode;
                return LEDMode.FromPayload(MessageType, value);
            });
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the power to be aplied to LED0, between 1 and 120.
    /// </summary>
    [DisplayName("LED0PowerPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the power to be aplied to LED0, between 1 and 120.")]
    public partial class CreateLED0PowerPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the power to be aplied to LED0, between 1 and 120.
        /// </summary>
        [Range(min: 1, max: 120)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the power to be aplied to LED0, between 1 and 120.")]
        public byte Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the power to be aplied to LED0, between 1 and 120.
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
        /// that sets the power to be aplied to LED0, between 1 and 120.
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
            return source.Select(_ => LED0Power.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the power to be aplied to LED1, between 1 and 120.
    /// </summary>
    [DisplayName("LED1PowerPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the power to be aplied to LED1, between 1 and 120.")]
    public partial class CreateLED1PowerPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the power to be aplied to LED1, between 1 and 120.
        /// </summary>
        [Range(min: 1, max: 120)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the power to be aplied to LED1, between 1 and 120.")]
        public byte Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the power to be aplied to LED1, between 1 and 120.
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
        /// that sets the power to be aplied to LED1, between 1 and 120.
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
            return source.Select(_ => LED1Power.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the	PWM frequency (Hz) of LED0's when in PWM mode, between 0.5 and 2000.
    /// </summary>
    [DisplayName("LED0PWMFrequencyPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the	PWM frequency (Hz) of LED0's when in PWM mode, between 0.5 and 2000.")]
    public partial class CreateLED0PWMFrequencyPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the	PWM frequency (Hz) of LED0's when in PWM mode, between 0.5 and 2000.
        /// </summary>
        [Range(min: 0.5, max: 2000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the	PWM frequency (Hz) of LED0's when in PWM mode, between 0.5 and 2000.")]
        public float Value { get; set; } = 0.5;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the	PWM frequency (Hz) of LED0's when in PWM mode, between 0.5 and 2000.
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
        /// that sets the	PWM frequency (Hz) of LED0's when in PWM mode, between 0.5 and 2000.
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
            return source.Select(_ => LED0PWMFrequency.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the	PWM duty cycle (%) of LED0's when in PWM mode, between 0.1 and 99.9.
    /// </summary>
    [DisplayName("LED0PWMDutyCyclePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the	PWM duty cycle (%) of LED0's when in PWM mode, between 0.1 and 99.9.")]
    public partial class CreateLED0PWMDutyCyclePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the	PWM duty cycle (%) of LED0's when in PWM mode, between 0.1 and 99.9.
        /// </summary>
        [Range(min: 0.1, max: 99.9)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the	PWM duty cycle (%) of LED0's when in PWM mode, between 0.1 and 99.9.")]
        public float Value { get; set; } = 0.1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the	PWM duty cycle (%) of LED0's when in PWM mode, between 0.1 and 99.9.
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
        /// that sets the	PWM duty cycle (%) of LED0's when in PWM mode, between 0.1 and 99.9.
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
            return source.Select(_ => LED0PWMDutyCycle.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the	PWM number of pulses of LED0's when in PWM mode, between 1 and 65535.
    /// </summary>
    [DisplayName("LED0PWMNumberPulsesPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the	PWM number of pulses of LED0's when in PWM mode, between 1 and 65535.")]
    public partial class CreateLED0PWMNumberPulsesPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the	PWM number of pulses of LED0's when in PWM mode, between 1 and 65535.
        /// </summary>
        [Range(min: 1, max: 65535)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the	PWM number of pulses of LED0's when in PWM mode, between 1 and 65535.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the	PWM number of pulses of LED0's when in PWM mode, between 1 and 65535.
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
        /// that sets the	PWM number of pulses of LED0's when in PWM mode, between 1 and 65535.
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
            return source.Select(_ => LED0PWMNumberPulses.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the	time on (milliseconds) of LED0's when in interval mode, between 1 and 65535.
    /// </summary>
    [DisplayName("LED0IntervalOnPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the	time on (milliseconds) of LED0's when in interval mode, between 1 and 65535.")]
    public partial class CreateLED0IntervalOnPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the	time on (milliseconds) of LED0's when in interval mode, between 1 and 65535.
        /// </summary>
        [Range(min: 1, max: 65535)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the	time on (milliseconds) of LED0's when in interval mode, between 1 and 65535.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the	time on (milliseconds) of LED0's when in interval mode, between 1 and 65535.
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
        /// that sets the	time on (milliseconds) of LED0's when in interval mode, between 1 and 65535.
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
            return source.Select(_ => LED0IntervalOn.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the	time off (milliseconds) of LED0's when in interval mode, between 1 and 65535.
    /// </summary>
    [DisplayName("LED0IntervalOffPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the	time off (milliseconds) of LED0's when in interval mode, between 1 and 65535.")]
    public partial class CreateLED0IntervalOffPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the	time off (milliseconds) of LED0's when in interval mode, between 1 and 65535.
        /// </summary>
        [Range(min: 1, max: 65535)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the	time off (milliseconds) of LED0's when in interval mode, between 1 and 65535.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the	time off (milliseconds) of LED0's when in interval mode, between 1 and 65535.
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
        /// that sets the	time off (milliseconds) of LED0's when in interval mode, between 1 and 65535.
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
            return source.Select(_ => LED0IntervalOff.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the	number of pulses of LED0's when in interval mode, between 1 and 65535.
    /// </summary>
    [DisplayName("LED0IntervalNumberPulsesPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the	number of pulses of LED0's when in interval mode, between 1 and 65535.")]
    public partial class CreateLED0IntervalNumberPulsesPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the	number of pulses of LED0's when in interval mode, between 1 and 65535.
        /// </summary>
        [Range(min: 1, max: 65535)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the	number of pulses of LED0's when in interval mode, between 1 and 65535.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the	number of pulses of LED0's when in interval mode, between 1 and 65535.
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
        /// that sets the	number of pulses of LED0's when in interval mode, between 1 and 65535.
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
            return source.Select(_ => LED0IntervalNumberPulses.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the	wait time between pulses (milliseconds) of LED0's when in interval mode, between 1 and 65535.
    /// </summary>
    [DisplayName("LED0IntervalTailPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the	wait time between pulses (milliseconds) of LED0's when in interval mode, between 1 and 65535.")]
    public partial class CreateLED0IntervalTailPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the	wait time between pulses (milliseconds) of LED0's when in interval mode, between 1 and 65535.
        /// </summary>
        [Range(min: 1, max: 65535)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the	wait time between pulses (milliseconds) of LED0's when in interval mode, between 1 and 65535.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the	wait time between pulses (milliseconds) of LED0's when in interval mode, between 1 and 65535.
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
        /// that sets the	wait time between pulses (milliseconds) of LED0's when in interval mode, between 1 and 65535.
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
            return source.Select(_ => LED0IntervalTail.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the	number of repetitions of LED0's pulse when in interval mode, between 1 and 65535.
    /// </summary>
    [DisplayName("LED0IntervalNumberRepeatsPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the	number of repetitions of LED0's pulse when in interval mode, between 1 and 65535.")]
    public partial class CreateLED0IntervalNumberRepeatsPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the	number of repetitions of LED0's pulse when in interval mode, between 1 and 65535.
        /// </summary>
        [Range(min: 1, max: 65535)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the	number of repetitions of LED0's pulse when in interval mode, between 1 and 65535.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the	number of repetitions of LED0's pulse when in interval mode, between 1 and 65535.
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
        /// that sets the	number of repetitions of LED0's pulse when in interval mode, between 1 and 65535.
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
            return source.Select(_ => LED0IntervalNumberRepeats.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the	PWM frequency (Hz) of LED1's when in PWM mode, between 0.5 and 2000.
    /// </summary>
    [DisplayName("LED1PWMFrequencyPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the	PWM frequency (Hz) of LED1's when in PWM mode, between 0.5 and 2000.")]
    public partial class CreateLED1PWMFrequencyPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the	PWM frequency (Hz) of LED1's when in PWM mode, between 0.5 and 2000.
        /// </summary>
        [Range(min: 0.5, max: 2000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the	PWM frequency (Hz) of LED1's when in PWM mode, between 0.5 and 2000.")]
        public float Value { get; set; } = 0.5;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the	PWM frequency (Hz) of LED1's when in PWM mode, between 0.5 and 2000.
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
        /// that sets the	PWM frequency (Hz) of LED1's when in PWM mode, between 0.5 and 2000.
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
            return source.Select(_ => LED1PWMFrequency.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the	PWM duty cycle (%) of LED1's when in PWM mode, between 0.1 and 99.9.
    /// </summary>
    [DisplayName("LED1PWMDutyCyclePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the	PWM duty cycle (%) of LED1's when in PWM mode, between 0.1 and 99.9.")]
    public partial class CreateLED1PWMDutyCyclePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the	PWM duty cycle (%) of LED1's when in PWM mode, between 0.1 and 99.9.
        /// </summary>
        [Range(min: 0.1, max: 99.9)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the	PWM duty cycle (%) of LED1's when in PWM mode, between 0.1 and 99.9.")]
        public float Value { get; set; } = 0.1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the	PWM duty cycle (%) of LED1's when in PWM mode, between 0.1 and 99.9.
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
        /// that sets the	PWM duty cycle (%) of LED1's when in PWM mode, between 0.1 and 99.9.
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
            return source.Select(_ => LED1PWMDutyCycle.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the	PWM number of pulses of LED1's when in PWM mode, between 1 and 65535.
    /// </summary>
    [DisplayName("LED1PWMNumberPulsesPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the	PWM number of pulses of LED1's when in PWM mode, between 1 and 65535.")]
    public partial class CreateLED1PWMNumberPulsesPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the	PWM number of pulses of LED1's when in PWM mode, between 1 and 65535.
        /// </summary>
        [Range(min: 1, max: 65535)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the	PWM number of pulses of LED1's when in PWM mode, between 1 and 65535.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the	PWM number of pulses of LED1's when in PWM mode, between 1 and 65535.
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
        /// that sets the	PWM number of pulses of LED1's when in PWM mode, between 1 and 65535.
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
            return source.Select(_ => LED1PWMNumberPulses.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the	time on (milliseconds) of LED1's when in interval mode, between 1 and 65535.
    /// </summary>
    [DisplayName("LED1IntervalOnPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the	time on (milliseconds) of LED1's when in interval mode, between 1 and 65535.")]
    public partial class CreateLED1IntervalOnPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the	time on (milliseconds) of LED1's when in interval mode, between 1 and 65535.
        /// </summary>
        [Range(min: 1, max: 65535)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the	time on (milliseconds) of LED1's when in interval mode, between 1 and 65535.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the	time on (milliseconds) of LED1's when in interval mode, between 1 and 65535.
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
        /// that sets the	time on (milliseconds) of LED1's when in interval mode, between 1 and 65535.
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
            return source.Select(_ => LED1IntervalOn.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the	time off (milliseconds) of LED1's when in interval mode, between 1 and 65535.
    /// </summary>
    [DisplayName("LED1IntervalOffPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the	time off (milliseconds) of LED1's when in interval mode, between 1 and 65535.")]
    public partial class CreateLED1IntervalOffPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the	time off (milliseconds) of LED1's when in interval mode, between 1 and 65535.
        /// </summary>
        [Range(min: 1, max: 65535)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the	time off (milliseconds) of LED1's when in interval mode, between 1 and 65535.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the	time off (milliseconds) of LED1's when in interval mode, between 1 and 65535.
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
        /// that sets the	time off (milliseconds) of LED1's when in interval mode, between 1 and 65535.
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
            return source.Select(_ => LED1IntervalOff.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the	number of pulses of LED1's when in interval mode, between 1 and 65535.
    /// </summary>
    [DisplayName("LED1IntervalNumberPulsesPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the	number of pulses of LED1's when in interval mode, between 1 and 65535.")]
    public partial class CreateLED1IntervalNumberPulsesPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the	number of pulses of LED1's when in interval mode, between 1 and 65535.
        /// </summary>
        [Range(min: 1, max: 65535)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the	number of pulses of LED1's when in interval mode, between 1 and 65535.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the	number of pulses of LED1's when in interval mode, between 1 and 65535.
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
        /// that sets the	number of pulses of LED1's when in interval mode, between 1 and 65535.
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
            return source.Select(_ => LED1IntervalNumberPulses.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the	wait time between pulses (milliseconds) of LED1's when in interval mode, between 1 and 65535.
    /// </summary>
    [DisplayName("LED1IntervalTailPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the	wait time between pulses (milliseconds) of LED1's when in interval mode, between 1 and 65535.")]
    public partial class CreateLED1IntervalTailPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the	wait time between pulses (milliseconds) of LED1's when in interval mode, between 1 and 65535.
        /// </summary>
        [Range(min: 1, max: 65535)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the	wait time between pulses (milliseconds) of LED1's when in interval mode, between 1 and 65535.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the	wait time between pulses (milliseconds) of LED1's when in interval mode, between 1 and 65535.
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
        /// that sets the	wait time between pulses (milliseconds) of LED1's when in interval mode, between 1 and 65535.
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
            return source.Select(_ => LED1IntervalTail.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the	number of repetitions of LED1's pulse when in interval mode, between 1 and 65535.
    /// </summary>
    [DisplayName("LED1IntervalNumberRepeatsPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the	number of repetitions of LED1's pulse when in interval mode, between 1 and 65535.")]
    public partial class CreateLED1IntervalNumberRepeatsPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the	number of repetitions of LED1's pulse when in interval mode, between 1 and 65535.
        /// </summary>
        [Range(min: 1, max: 65535)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the	number of repetitions of LED1's pulse when in interval mode, between 1 and 65535.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the	number of repetitions of LED1's pulse when in interval mode, between 1 and 65535.
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
        /// that sets the	number of repetitions of LED1's pulse when in interval mode, between 1 and 65535.
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
            return source.Select(_ => LED1IntervalNumberRepeats.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that get the real	PWM frequency (Hz) of LED0's when in PWM mode.
    /// </summary>
    [DisplayName("LED0PWMFrequencyRealPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that get the real	PWM frequency (Hz) of LED0's when in PWM mode.")]
    public partial class CreateLED0PWMFrequencyRealPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that get the real	PWM frequency (Hz) of LED0's when in PWM mode.
        /// </summary>
        [Description("The value that get the real	PWM frequency (Hz) of LED0's when in PWM mode.")]
        public float Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that get the real	PWM frequency (Hz) of LED0's when in PWM mode.
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
        /// that get the real	PWM frequency (Hz) of LED0's when in PWM mode.
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
            return source.Select(_ => LED0PWMFrequencyReal.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that get the real	duty cycle (%) of LED0's when in PWM mode.
    /// </summary>
    [DisplayName("LED0PWMDutyCycleRealPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that get the real	duty cycle (%) of LED0's when in PWM mode.")]
    public partial class CreateLED0PWMDutyCycleRealPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that get the real	duty cycle (%) of LED0's when in PWM mode.
        /// </summary>
        [Description("The value that get the real	duty cycle (%) of LED0's when in PWM mode.")]
        public float Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that get the real	duty cycle (%) of LED0's when in PWM mode.
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
        /// that get the real	duty cycle (%) of LED0's when in PWM mode.
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
            return source.Select(_ => LED0PWMDutyCycleReal.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that get the real	PWM frequency (Hz) of LED1's when in PWM mode.
    /// </summary>
    [DisplayName("LED1PWMFrequencyRealPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that get the real	PWM frequency (Hz) of LED1's when in PWM mode.")]
    public partial class CreateLED1PWMFrequencyRealPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that get the real	PWM frequency (Hz) of LED1's when in PWM mode.
        /// </summary>
        [Description("The value that get the real	PWM frequency (Hz) of LED1's when in PWM mode.")]
        public float Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that get the real	PWM frequency (Hz) of LED1's when in PWM mode.
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
        /// that get the real	PWM frequency (Hz) of LED1's when in PWM mode.
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
            return source.Select(_ => LED1PWMFrequencyReal.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that get the real	duty cycle (%) of LED1's when in PWM mode.
    /// </summary>
    [DisplayName("LED1PWMDutyCycleRealPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that get the real	duty cycle (%) of LED1's when in PWM mode.")]
    public partial class CreateLED1PWMDutyCycleRealPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that get the real	duty cycle (%) of LED1's when in PWM mode.
        /// </summary>
        [Description("The value that get the real	duty cycle (%) of LED1's when in PWM mode.")]
        public float Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that get the real	duty cycle (%) of LED1's when in PWM mode.
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
        /// that get the real	duty cycle (%) of LED1's when in PWM mode.
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
            return source.Select(_ => LED1PWMDutyCycleReal.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that write the state of the auxiliar digital output bit.
    /// </summary>
    [DisplayName("AuxDigitalOutputStatePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that write the state of the auxiliar digital output bit.")]
    public partial class CreateAuxDigitalOutputStatePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that write the state of the auxiliar digital output bit.
        /// </summary>
        [Description("The value that write the state of the auxiliar digital output bit.")]
        public AuxDigitalOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that write the state of the auxiliar digital output bit.
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
        /// that write the state of the auxiliar digital output bit.
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
    /// that sets the power to be aplied to auxiliary LED, between 1 and 120.
    /// </summary>
    [DisplayName("AuxLEDPowerPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the power to be aplied to auxiliary LED, between 1 and 120.")]
    public partial class CreateAuxLEDPowerPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the power to be aplied to auxiliary LED, between 1 and 120.
        /// </summary>
        [Range(min: 1, max: 120)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the power to be aplied to auxiliary LED, between 1 and 120.")]
        public byte Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the power to be aplied to auxiliary LED, between 1 and 120.
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
        /// that sets the power to be aplied to auxiliary LED, between 1 and 120.
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
            return source.Select(_ => AuxLEDPower.FromPayload(MessageType, Value));
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
            DI0TriggerConfig dI0Trigger,
            DI1TriggerConfig dI1Trigger)
        {
            DI0Trigger = dI0Trigger;
            DI1Trigger = dI1Trigger;
        }

        /// <summary>
        /// Configuration of the DIO input pin.
        /// </summary>
        public DI0TriggerConfig DI0Trigger;

        /// <summary>
        /// Configuration of the DI1 input pin.
        /// </summary>
        public DI1TriggerConfig DI1Trigger;
    }

    /// <summary>
    /// Represents the payload of the LEDMode register.
    /// </summary>
    public struct LEDModePayload
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LEDModePayload"/> structure.
        /// </summary>
        /// <param name="lED0Mode">Sets the configuration mode of the LED0 when behavior is enabled.</param>
        /// <param name="lED1Mode">Sets the configuration mode of the LED1 when behavior is enabled.</param>
        public LEDModePayload(
            LED0ModeConfig lED0Mode,
            LED1ModeConfig lED1Mode)
        {
            LED0Mode = lED0Mode;
            LED1Mode = lED1Mode;
        }

        /// <summary>
        /// Sets the configuration mode of the LED0 when behavior is enabled.
        /// </summary>
        public LED0ModeConfig LED0Mode;

        /// <summary>
        /// Sets the configuration mode of the LED1 when behavior is enabled.
        /// </summary>
        public LED1ModeConfig LED1Mode;
    }

    /// <summary>
    /// Specifies the LEDs state.
    /// </summary>
    [Flags]
    public enum LEDs : byte
    {
        LED0On = 0x1,
        LED1On = 0x2,
        LED0Off = 0x4,
        LED1Off = 0x8
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
        AUX0High = 0x1,
        AUX1High = 0x2,
        AUX0Low = 0x4,
        AUX1Low = 0x8
    }

    /// <summary>
    /// Specifies the state of port digital output lines.
    /// </summary>
    [Flags]
    public enum DigitalOutputs : byte
    {
        DO0High = 0x1,
        DO1High = 0x2,
        DO0Low = 0x4,
        DO1Low = 0x8
    }

    /// <summary>
    /// The events that can be enabled/disabled.
    /// </summary>
    [Flags]
    public enum LedArrayEvents : byte
    {
        EnableLED = 0x1,
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
    public enum DI0TriggerConfig : byte
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
    /// Available configurations when using digital inputs as an acquisition trigger.
    /// </summary>
    public enum DI1TriggerConfig : byte
    {
        Led0EnablePower = 0,
        Led0EnableBehavior = 16,
        Led0EnableLed = 32,
        Led1EnablePower = 48,
        Led1EnableBehavior = 64,
        Led1EnableLed = 80,
        None = 96
    }

    /// <summary>
    /// Available configurations modes when LED behavior is enabled.
    /// </summary>
    public enum LED0ModeConfig : byte
    {
        PWM = 0,
        Interval = 1
    }

    /// <summary>
    /// Available configurations modes when LED behavior is enabled.
    /// </summary>
    public enum LED1ModeConfig : byte
    {
        PWM = 0,
        Interval = 16
    }
}
