using Bonsai.Harp;
using System.Threading.Tasks;

namespace Harp.LedArray
{
    /// <inheritdoc/>
    public partial class Device
    {
        /// <summary>
        /// Initializes a new instance of the asynchronous API to configure and interface
        /// with LedArray devices on the specified serial port.
        /// </summary>
        /// <param name="portName">
        /// The name of the serial port used to communicate with the Harp device.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous initialization operation. The value of
        /// the <see cref="Task{TResult}.Result"/> parameter contains a new instance of
        /// the <see cref="AsyncDevice"/> class.
        /// </returns>
        public static async Task<AsyncDevice> CreateAsync(string portName)
        {
            var device = new AsyncDevice(portName);
            var whoAmI = await device.ReadWhoAmIAsync();
            if (whoAmI != Device.WhoAmI)
            {
                var errorMessage = string.Format(
                    "The device ID {1} on {0} was unexpected. Check whether a LedArray device is connected to the specified serial port.",
                    portName, whoAmI);
                throw new HarpException(errorMessage);
            }

            return device;
        }
    }

    /// <summary>
    /// Represents an asynchronous API to configure and interface with LedArray devices.
    /// </summary>
    public partial class AsyncDevice : Bonsai.Harp.AsyncDevice
    {
        internal AsyncDevice(string portName)
            : base(portName)
        {
        }

        /// <summary>
        /// Asynchronously reads the contents of the EnablePower register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<LEDs> ReadEnablePowerAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnablePower.Address));
            return EnablePower.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EnablePower register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<LEDs>> ReadTimestampedEnablePowerAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnablePower.Address));
            return EnablePower.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EnablePower register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEnablePowerAsync(LEDs value)
        {
            var request = EnablePower.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EnableBehavior register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<LEDs> ReadEnableBehaviorAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableBehavior.Address));
            return EnableBehavior.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EnableBehavior register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<LEDs>> ReadTimestampedEnableBehaviorAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableBehavior.Address));
            return EnableBehavior.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EnableBehavior register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEnableBehaviorAsync(LEDs value)
        {
            var request = EnableBehavior.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EnableLED register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<LEDs> ReadEnableLEDAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableLED.Address));
            return EnableLED.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EnableLED register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<LEDs>> ReadTimestampedEnableLEDAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableLED.Address));
            return EnableLED.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EnableLED register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEnableLEDAsync(LEDs value)
        {
            var request = EnableLED.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DigitalInputState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalInputs> ReadDigitalInputStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalInputState.Address));
            return DigitalInputState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DigitalInputState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalInputs>> ReadTimestampedDigitalInputStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalInputState.Address));
            return DigitalInputState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DigitalOutputSync register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputSyncPayload> ReadDigitalOutputSyncAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalOutputSync.Address));
            return DigitalOutputSync.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DigitalOutputSync register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputSyncPayload>> ReadTimestampedDigitalOutputSyncAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalOutputSync.Address));
            return DigitalOutputSync.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DigitalOutputSync register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDigitalOutputSyncAsync(DigitalOutputSyncPayload value)
        {
            var request = DigitalOutputSync.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DigitalInputTrigger register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalInputTriggerPayload> ReadDigitalInputTriggerAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalInputTrigger.Address));
            return DigitalInputTrigger.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DigitalInputTrigger register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalInputTriggerPayload>> ReadTimestampedDigitalInputTriggerAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalInputTrigger.Address));
            return DigitalInputTrigger.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DigitalInputTrigger register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDigitalInputTriggerAsync(DigitalInputTriggerPayload value)
        {
            var request = DigitalInputTrigger.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LEDMode register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<LEDModePayload> ReadLEDModeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(LEDMode.Address));
            return LEDMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LEDMode register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<LEDModePayload>> ReadTimestampedLEDModeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(LEDMode.Address));
            return LEDMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the LEDMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLEDModeAsync(LEDModePayload value)
        {
            var request = LEDMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LED0Power register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadLED0PowerAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(LED0Power.Address));
            return LED0Power.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LED0Power register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedLED0PowerAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(LED0Power.Address));
            return LED0Power.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the LED0Power register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLED0PowerAsync(byte value)
        {
            var request = LED0Power.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LED1Power register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadLED1PowerAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(LED1Power.Address));
            return LED1Power.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LED1Power register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedLED1PowerAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(LED1Power.Address));
            return LED1Power.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the LED1Power register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLED1PowerAsync(byte value)
        {
            var request = LED1Power.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LED0PWMFrequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadLED0PWMFrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(LED0PWMFrequency.Address));
            return LED0PWMFrequency.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LED0PWMFrequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedLED0PWMFrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(LED0PWMFrequency.Address));
            return LED0PWMFrequency.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the LED0PWMFrequency register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLED0PWMFrequencyAsync(float value)
        {
            var request = LED0PWMFrequency.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LED0PWMDutyCycle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadLED0PWMDutyCycleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(LED0PWMDutyCycle.Address));
            return LED0PWMDutyCycle.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LED0PWMDutyCycle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedLED0PWMDutyCycleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(LED0PWMDutyCycle.Address));
            return LED0PWMDutyCycle.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the LED0PWMDutyCycle register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLED0PWMDutyCycleAsync(float value)
        {
            var request = LED0PWMDutyCycle.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LED0PWMNumberPulses register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadLED0PWMNumberPulsesAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(LED0PWMNumberPulses.Address));
            return LED0PWMNumberPulses.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LED0PWMNumberPulses register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedLED0PWMNumberPulsesAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(LED0PWMNumberPulses.Address));
            return LED0PWMNumberPulses.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the LED0PWMNumberPulses register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLED0PWMNumberPulsesAsync(ushort value)
        {
            var request = LED0PWMNumberPulses.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LED0IntervalOn register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadLED0IntervalOnAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(LED0IntervalOn.Address));
            return LED0IntervalOn.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LED0IntervalOn register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedLED0IntervalOnAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(LED0IntervalOn.Address));
            return LED0IntervalOn.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the LED0IntervalOn register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLED0IntervalOnAsync(ushort value)
        {
            var request = LED0IntervalOn.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LED0IntervalOff register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadLED0IntervalOffAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(LED0IntervalOff.Address));
            return LED0IntervalOff.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LED0IntervalOff register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedLED0IntervalOffAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(LED0IntervalOff.Address));
            return LED0IntervalOff.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the LED0IntervalOff register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLED0IntervalOffAsync(ushort value)
        {
            var request = LED0IntervalOff.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LED0IntervalNumberPulses register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadLED0IntervalNumberPulsesAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(LED0IntervalNumberPulses.Address));
            return LED0IntervalNumberPulses.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LED0IntervalNumberPulses register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedLED0IntervalNumberPulsesAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(LED0IntervalNumberPulses.Address));
            return LED0IntervalNumberPulses.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the LED0IntervalNumberPulses register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLED0IntervalNumberPulsesAsync(ushort value)
        {
            var request = LED0IntervalNumberPulses.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LED0IntervalTail register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadLED0IntervalTailAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(LED0IntervalTail.Address));
            return LED0IntervalTail.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LED0IntervalTail register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedLED0IntervalTailAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(LED0IntervalTail.Address));
            return LED0IntervalTail.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the LED0IntervalTail register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLED0IntervalTailAsync(ushort value)
        {
            var request = LED0IntervalTail.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LED0IntervalNumberRepeats register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadLED0IntervalNumberRepeatsAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(LED0IntervalNumberRepeats.Address));
            return LED0IntervalNumberRepeats.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LED0IntervalNumberRepeats register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedLED0IntervalNumberRepeatsAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(LED0IntervalNumberRepeats.Address));
            return LED0IntervalNumberRepeats.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the LED0IntervalNumberRepeats register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLED0IntervalNumberRepeatsAsync(ushort value)
        {
            var request = LED0IntervalNumberRepeats.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LED1PWMFrequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadLED1PWMFrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(LED1PWMFrequency.Address));
            return LED1PWMFrequency.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LED1PWMFrequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedLED1PWMFrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(LED1PWMFrequency.Address));
            return LED1PWMFrequency.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the LED1PWMFrequency register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLED1PWMFrequencyAsync(float value)
        {
            var request = LED1PWMFrequency.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LED1PWMDutyCycle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadLED1PWMDutyCycleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(LED1PWMDutyCycle.Address));
            return LED1PWMDutyCycle.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LED1PWMDutyCycle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedLED1PWMDutyCycleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(LED1PWMDutyCycle.Address));
            return LED1PWMDutyCycle.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the LED1PWMDutyCycle register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLED1PWMDutyCycleAsync(float value)
        {
            var request = LED1PWMDutyCycle.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LED1PWMNumberPulses register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadLED1PWMNumberPulsesAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(LED1PWMNumberPulses.Address));
            return LED1PWMNumberPulses.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LED1PWMNumberPulses register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedLED1PWMNumberPulsesAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(LED1PWMNumberPulses.Address));
            return LED1PWMNumberPulses.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the LED1PWMNumberPulses register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLED1PWMNumberPulsesAsync(ushort value)
        {
            var request = LED1PWMNumberPulses.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LED1IntervalOn register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadLED1IntervalOnAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(LED1IntervalOn.Address));
            return LED1IntervalOn.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LED1IntervalOn register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedLED1IntervalOnAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(LED1IntervalOn.Address));
            return LED1IntervalOn.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the LED1IntervalOn register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLED1IntervalOnAsync(ushort value)
        {
            var request = LED1IntervalOn.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LED1IntervalOff register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadLED1IntervalOffAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(LED1IntervalOff.Address));
            return LED1IntervalOff.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LED1IntervalOff register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedLED1IntervalOffAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(LED1IntervalOff.Address));
            return LED1IntervalOff.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the LED1IntervalOff register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLED1IntervalOffAsync(ushort value)
        {
            var request = LED1IntervalOff.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LED1IntervalNumberPulses register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadLED1IntervalNumberPulsesAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(LED1IntervalNumberPulses.Address));
            return LED1IntervalNumberPulses.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LED1IntervalNumberPulses register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedLED1IntervalNumberPulsesAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(LED1IntervalNumberPulses.Address));
            return LED1IntervalNumberPulses.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the LED1IntervalNumberPulses register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLED1IntervalNumberPulsesAsync(ushort value)
        {
            var request = LED1IntervalNumberPulses.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LED1IntervalTail register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadLED1IntervalTailAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(LED1IntervalTail.Address));
            return LED1IntervalTail.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LED1IntervalTail register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedLED1IntervalTailAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(LED1IntervalTail.Address));
            return LED1IntervalTail.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the LED1IntervalTail register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLED1IntervalTailAsync(ushort value)
        {
            var request = LED1IntervalTail.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LED1IntervalNumberRepeats register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadLED1IntervalNumberRepeatsAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(LED1IntervalNumberRepeats.Address));
            return LED1IntervalNumberRepeats.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LED1IntervalNumberRepeats register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedLED1IntervalNumberRepeatsAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(LED1IntervalNumberRepeats.Address));
            return LED1IntervalNumberRepeats.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the LED1IntervalNumberRepeats register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLED1IntervalNumberRepeatsAsync(ushort value)
        {
            var request = LED1IntervalNumberRepeats.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LED0PWMFrequencyReal register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadLED0PWMFrequencyRealAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(LED0PWMFrequencyReal.Address));
            return LED0PWMFrequencyReal.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LED0PWMFrequencyReal register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedLED0PWMFrequencyRealAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(LED0PWMFrequencyReal.Address));
            return LED0PWMFrequencyReal.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LED0PWMDutyCycleReal register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadLED0PWMDutyCycleRealAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(LED0PWMDutyCycleReal.Address));
            return LED0PWMDutyCycleReal.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LED0PWMDutyCycleReal register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedLED0PWMDutyCycleRealAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(LED0PWMDutyCycleReal.Address));
            return LED0PWMDutyCycleReal.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LED1PWMFrequencyReal register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadLED1PWMFrequencyRealAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(LED1PWMFrequencyReal.Address));
            return LED1PWMFrequencyReal.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LED1PWMFrequencyReal register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedLED1PWMFrequencyRealAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(LED1PWMFrequencyReal.Address));
            return LED1PWMFrequencyReal.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LED1PWMDutyCycleReal register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadLED1PWMDutyCycleRealAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(LED1PWMDutyCycleReal.Address));
            return LED1PWMDutyCycleReal.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LED1PWMDutyCycleReal register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedLED1PWMDutyCycleRealAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(LED1PWMDutyCycleReal.Address));
            return LED1PWMDutyCycleReal.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the AuxDigitalOutputState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<AuxDigitalOutputs> ReadAuxDigitalOutputStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AuxDigitalOutputState.Address));
            return AuxDigitalOutputState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the AuxDigitalOutputState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<AuxDigitalOutputs>> ReadTimestampedAuxDigitalOutputStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AuxDigitalOutputState.Address));
            return AuxDigitalOutputState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the AuxDigitalOutputState register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteAuxDigitalOutputStateAsync(AuxDigitalOutputs value)
        {
            var request = AuxDigitalOutputState.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the AuxLEDPower register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadAuxLEDPowerAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AuxLEDPower.Address));
            return AuxLEDPower.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the AuxLEDPower register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedAuxLEDPowerAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AuxLEDPower.Address));
            return AuxLEDPower.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the AuxLEDPower register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteAuxLEDPowerAsync(byte value)
        {
            var request = AuxLEDPower.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DigitalOutputState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadDigitalOutputStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalOutputState.Address));
            return DigitalOutputState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DigitalOutputState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedDigitalOutputStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalOutputState.Address));
            return DigitalOutputState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DigitalOutputState register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDigitalOutputStateAsync(DigitalOutputs value)
        {
            var request = DigitalOutputState.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EnableEvents register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<LedArrayEvents> ReadEnableEventsAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableEvents.Address));
            return EnableEvents.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EnableEvents register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<LedArrayEvents>> ReadTimestampedEnableEventsAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableEvents.Address));
            return EnableEvents.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EnableEvents register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEnableEventsAsync(LedArrayEvents value)
        {
            var request = EnableEvents.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }
    }
}
