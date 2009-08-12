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
    public class PropertyHandlerDictionaryBuilderTests
    {
        [Fact]
        public void PropertyHandlerDictionaryBuilder_NamedT_PropertyNameNull_ThrowsException()
        {
            var handlerDictionary = new PropertyHandlerDictionary();
            var target = new PropertyHandlerDictionaryBuilder(handlerDictionary);

            Assert.Throws<ArgumentNullException>(() =>
            {
                target.Named<string>(null, a => { });
            });
        }

        [Fact]
        public void PropertyHandlerDictionaryBuilder_NamedT_HandlerNull_ThrowsException()
        {
            var handlerDictionary = new PropertyHandlerDictionary();
            var target = new PropertyHandlerDictionaryBuilder(handlerDictionary);

            Assert.Throws<ArgumentNullException>(() =>
            {
                target.Named<string>("key", null);
            });
        }

        [Fact]
        public void PropertyHandlerDictionaryBuilder_NamedT_PropertyName_Handler_AddsHandler()
        {
            var handlerDictionary = new PropertyHandlerDictionary();
            var target = new PropertyHandlerDictionaryBuilder(handlerDictionary);
            var keyName = "key";
            var actionRunFlag = false;
            Action<string> handler = (a) => { actionRunFlag = true; };

            target.Named<string>(keyName, handler);

            Assert.True(handlerDictionary.ContainsKey(keyName));
            handlerDictionary[keyName]("value");
            Assert.True(actionRunFlag);
        }


        [Fact]
        public void PropertyHandlerDictionaryBuilder_Named_PropertyNameNull_ThrowsException()
        {
            var handlerDictionary = new PropertyHandlerDictionary();
            var target = new PropertyHandlerDictionaryBuilder(handlerDictionary);

            Assert.Throws<ArgumentNullException>(() =>
            {
                target.Named(null, a => { });
            });
        }

        [Fact]
        public void PropertyHandlerDictionaryBuilder_Named_HandlerNull_ThrowsException()
        {
            var handlerDictionary = new PropertyHandlerDictionary();
            var target = new PropertyHandlerDictionaryBuilder(handlerDictionary);

            Assert.Throws<ArgumentNullException>(() =>
            {
                target.Named("key", null);
            });
        }

        [Fact]
        public void PropertyHandlerDictionaryBuilder_Named_PropertyName_Handler_AddsHandler()
        {
            var handlerDictionary = new PropertyHandlerDictionary();
            var target = new PropertyHandlerDictionaryBuilder(handlerDictionary);
            var keyName = "key";
            Action<object> handler = (a) => { };

            target.Named(keyName, handler);

            Assert.True(handlerDictionary.ContainsKey(keyName));
            Assert.Equal(handler, handlerDictionary[keyName]);
        }
    }
}
