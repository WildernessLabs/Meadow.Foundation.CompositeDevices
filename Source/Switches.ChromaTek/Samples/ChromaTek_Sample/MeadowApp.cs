using Meadow;
using Meadow.Devices;
using Meadow.Foundation.Switches.ChromaTek;
using Meadow.Units;
using System;
using System.Threading.Tasks;

namespace Switches.ChromaTek_Sample
{
    public class MeadowApp : App<F7FeatherV2>
    {
        //<!=SNIP=>

        private MomentaryButton _button = default!;
        private readonly Color _normalColor = Color.Green;
        private readonly Color _pressedColor = Color.Red;

        public override Task Initialize()
        {
            Resolver.Log.Info("Initialize...");

            var bus = Device.CreateSpiBus(new Frequency(2.5, Frequency.UnitType.Megahertz));
            _button = new MomentaryButton(Device.Pins.D04, Meadow.Hardware.ResistorMode.InternalPullUp, bus);
            _button.SetColor(_normalColor);
            _button.Clicked += OnButtonClicked;

            return Task.CompletedTask;
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            Resolver.Log.Info("Click");
            Task.Run(async () =>
            {
                _button.SetColor(_pressedColor);
                await Task.Delay(3000);
                _button.SetColor(_normalColor);
            });
        }

        //<!=SNOP=>
    }
}