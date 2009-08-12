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
    public class ArgumentReflectorObjectExtensionsTests
    {
        [Fact]
        public void ArgumentReflectorExtensions_Reflect_HandlerDictionaryNull_ThrowsException()
        {
            var target = new { key = "value" };

            Assert.Throws<ArgumentNullException>(() =>
            {
                target.Reflect((PropertyHandlerDictionary)null);
            });
        }

        [Fact]
        public void ArgumentReflectorExtensions_Reflect_HandlerDictionaryBuilderNull_ThrowsException()
        {
            var target = new { key = "value" };

            Assert.Throws<ArgumentNullException>(() =>
            {
                target.Reflect((Action<PropertyHandlerDictionaryBuilder>)null);
            });
        }

        [Fact]
        public void ArgumentReflectorExtensions_Reflect_Verify_HandlerEffectivelyRun()
        {
            var passedValue = "value";
            var reflectedValue = String.Empty;
            var target = new { key = passedValue };

            target.Reflect(new PropertyHandlerDictionary
            {
                { "key", v => reflectedValue = (string)v }
            });

            Assert.Equal(passedValue, reflectedValue);
        }

        [Fact]
        public void ArgumentReflectorExtensions_Reflect_HandlerDictionaryBuilder_ImplicitCaseInsensitive_DifferingCases_Verify_Handler_Run_PassedPropValue()
        {
            var passedValue = "value";
            var reflectedValue = String.Empty;
            var target = new { KEY = passedValue };

            target.Reflect(prop =>
            {
                prop.Named<string>("key", v => reflectedValue = v);
            });

            Assert.Equal(passedValue, reflectedValue);
        }

        [Fact]
        public void ArgumentReflectorExtensions_Reflect_HandlerDictionaryBuilder_ImplicitCaseInsensitive_DifferingCases_Reversed_Verify_Handler_Run_PassedPropValue()
        {
            var passedValue = "value";
            var reflectedValue = String.Empty;
            var target = new { key = passedValue };

            target.Reflect(prop =>
            {
                prop.Named<string>("KEY", v => reflectedValue = v);
            });

            Assert.Equal(passedValue, reflectedValue);
        }

        [Fact]
        public void ArgumentReflectorExtensions_Reflect_HandlerDictionaryBuilder_ExplicitCaseInsensitive_DifferingCases_Verify_Handler_Run_PassedPropValue()
        {
            var passedValue = "value";
            var reflectedValue = String.Empty;
            var target = new { KEY = passedValue };

            target.Reflect(false, prop =>
            {
                prop.Named<string>("key", v => reflectedValue = v);
            });

            Assert.Equal(passedValue, reflectedValue);
        }

        [Fact]
        public void ArgumentReflectorExtensions_Reflect_HandlerDictionaryBuilder_ExplicitCaseInsensitive_Reversed_DifferingCases_Verify_Handler_Run_PassedPropValue()
        {
            var passedValue = "value";
            var reflectedValue = String.Empty;
            var target = new { key = passedValue };

            target.Reflect(false, prop =>
            {
                prop.Named<string>("KEY", v => reflectedValue = v);
            });

            Assert.Equal(passedValue, reflectedValue);
        }


        [Fact]
        public void ArgumentReflectorExtensions_Reflect_HandlerDictionaryBuilder_ExplicitCaseSensitive_DifferingCases_Verify_Handler_Not_Run()
        {
            var passedValue = "value";
            var reflectedValue = String.Empty;
            var target = new { KEY = passedValue };

            target.Reflect(true, prop =>
            {
                prop.Named<string>("key", v => reflectedValue = v);
            });

            Assert.NotEqual(passedValue, reflectedValue);
        }

        [Fact]
        public void ArgumentReflectorExtensions_Reflect_HandlerDictionaryBuilder_ExplicitCaseSensitive_DifferingCases_Reversed_Verify_Handler_Not_Run()
        {
            var passedValue = "value";
            var reflectedValue = String.Empty;
            var target = new { key = passedValue };

            target.Reflect(true, prop =>
            {
                prop.Named<string>("KEY", v => reflectedValue = v);
            });

            Assert.NotEqual(passedValue, reflectedValue);
        }

        [Fact]
        public void ArgumentReflectorExtensions_Reflect_HandlerDictionary_ImplicitCaseInsensitive_DifferingCases_Verify_Handler_Run_PassedPropValue()
        {
            var passedValue = "value";
            var reflectedValue = String.Empty;
            var target = new { KEY = passedValue };

            target.Reflect(new PropertyHandlerDictionary
            {
                { "key", v => reflectedValue = (string)v }
            });

            Assert.Equal(passedValue, reflectedValue);
        }

        [Fact]
        public void ArgumentReflectorExtensions_Reflect_HandlerDictionary_ExplicitCaseInsensitive_DifferingCases_Verify_Handler_Run_PassedPropValue()
        {
            var passedValue = "value";
            var reflectedValue = String.Empty;
            var target = new { KEY = passedValue };

            target.Reflect(false, new PropertyHandlerDictionary
            {
                { "key", v => reflectedValue = (string)v }
            });

            Assert.Equal(passedValue, reflectedValue);
        }

        [Fact]
        public void ArgumentReflectorExtensions_Reflect_HandlerDictionary_PropertyBuilder_ExplicitCaseSensitive_DifferingCases_Verify_Handler_Not_Run()
        {
            var passedValue = "value";
            var reflectedValue = String.Empty;
            var target = new { KEY = passedValue };

            target.Reflect(true, new PropertyHandlerDictionary
            {
                { "key", v => reflectedValue = (string)v }
            });

            Assert.NotEqual(passedValue, reflectedValue);
        }
    }
}

