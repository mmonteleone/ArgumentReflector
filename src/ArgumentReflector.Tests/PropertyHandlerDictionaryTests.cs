#region license
/* ArgumentReflector
 * 
 * Copyright (C) 2008 Michael Monteleone (http://michaelmonteleone.net)
 *
 * Permission is hereby granted, free of charge, to any person obtaining a 
 * copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation 
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the 
 * Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included 
 * in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS 
 * OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
 * THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 * DEALINGS IN THE SOFTWARE.
 */
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ArgumentReflector.Tests
{
    public class PropertyHandlerDictionaryTests
    {
        [Fact]
        public void PropertyHandlerDictionary_AddTypedHandlerT_NameNull_ThrowsException()
        {
            var target = new PropertyHandlerDictionary();

            Assert.Throws(typeof(ArgumentNullException), () =>
            {
                target.AddTypedHandler<string>(null, a => { });
            });
        }

        [Fact]
        public void PropertyHandlerDictionary_AddTypedHandlerT_HandlerNull_ThrowsException()
        {
            var target = new PropertyHandlerDictionary();

            Assert.Throws(typeof(ArgumentNullException), () =>
            {
                target.AddTypedHandler<string>("key", null);
            });
        }

        [Fact]
        public void PropertyHandlerDictionary_AddTypedHandlerT_Verify_Adds_Handler()
        {
            var target = new PropertyHandlerDictionary();
            var keyName = "key";

            target.AddTypedHandler<string>(keyName, prop => { });

            Assert.Equal(1, target.Keys.Count);
            Assert.True(target.ContainsKey(keyName));
        }
    }
}
