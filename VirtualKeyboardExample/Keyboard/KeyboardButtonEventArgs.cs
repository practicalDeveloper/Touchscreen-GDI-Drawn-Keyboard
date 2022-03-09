using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keyboard
{
    public class KeyboardButtonEventArgs: EventArgs
    {
        /// <summary>
        /// Creates a new <see cref="VirtualKbButton"/>
        /// </summary>
        /// <param name="button">Related keyboard button</param>
        public KeyboardButtonEventArgs(VirtualKbButton button)
        {
            _button = button;
        }

        private VirtualKbButton _button;

        /// <summary>
        /// Gets the <see cref="VirtualKbButton"/> related to the event
        /// </summary>
        public VirtualKbButton Button
        {
            get { return _button; }
        }

    }
}
