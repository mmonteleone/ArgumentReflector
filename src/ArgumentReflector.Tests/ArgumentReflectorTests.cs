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
    public class ArgumentReflectorTests
    {
        [Fact]
        public void ArgumentReflector_Reflect_HandlerDictionary_Null_ThrowsException()
        {
            var target = new ArgumentReflector(new { });

            Assert.Throws<ArgumentNullException>(() =>
            {
                target.Reflect((PropertyHandlerDictionary)null);
            });
        }

        [Fact]
        public void ArgumentReflector_Reflect_HandlerDictionaryBuilder_Null_ThrowsException()
        {
            var obj = new { };
            var target = new ArgumentReflector(obj);

            Assert.Throws<ArgumentNullException>(() =>
            {
                target.Reflect((Action<PropertyHandlerDictionaryBuilder>)null);
            });
        }

        [Fact]
        public void ArgumentReflector_Reflect_TargetNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var target = new ArgumentReflector(null);
            });
        }

        [Fact]
        public void ArgumentReflector_Reflect_HandlerDictionaryNull_ThrowsException()
        {
            var obj = new { };
            var target = new ArgumentReflector(obj);

            Assert.Throws<ArgumentNullException>(() =>
            {
                target.Reflect((PropertyHandlerDictionary)null);
            });
        }

        [Fact]
        public void ArgumentReflector_Reflect_Verify_HandlerRun_And_PassedPropertyValue()
        {
            var passedValue = "value";
            var relfectedValue = String.Empty;
            var target = new ArgumentReflector(new { key = passedValue });

            target.Reflect(new PropertyHandlerDictionary
            {
                { "key", v => relfectedValue = (string)v }
            });

            Assert.Equal(passedValue, relfectedValue);
        }

        [Fact]
        public void ArgumentReflector_Reflect_ImplicitCaseInsensitivity_UpperCaseHandlerKey_LowerCasePropName_Verify_HandlerRun_PassedPropValue()
        {
            var passedValue = "value";
            var relfectedValue = String.Empty;
            var target = new ArgumentReflector(new { KEY = passedValue });

            target.Reflect(new PropertyHandlerDictionary
            {
                { "key", v => relfectedValue = (string)v }
            });

            Assert.Equal(passedValue, relfectedValue);
        }

        [Fact]
        public void ArgumentReflector_Reflect_ExplicitCaseInsensitivity_UpperCaseHandlerKey_LowerCasePropName_Verify_HandlerRun_PassedPropValue()
        {
            var passedValue = "value";
            var relfectedValue = String.Empty;
            var target = new ArgumentReflector(new { KEY = passedValue }, false);

            target.Reflect(new PropertyHandlerDictionary
            {
                { "key", v => relfectedValue = (string)v }
            });

            Assert.Equal(passedValue, relfectedValue);
        }

        [Fact]
        public void ArgumentReflector_Reflect_ImplicitCaseInsensitivity_LowerCaseHandlerKey_LowerCasePropName_Verify_HandlerRun_PassedPropValue()
        {
            var passedValue = "value";
            var relfectedValue = String.Empty;
            var target = new ArgumentReflector(new { key = passedValue });

            target.Reflect(new PropertyHandlerDictionary
            {
                { "key", v => relfectedValue = (string)v }
            });

            Assert.Equal(passedValue, relfectedValue);
        }

        [Fact]
        public void ArgumentReflector_Reflect_ExplicitCaseInsensitivity_LowerCaseHandlerKey_LowerCasePropName_Verify_HandlerRun_PassedPropValue()
        {
            var passedValue = "value";
            var relfectedValue = String.Empty;
            var target = new ArgumentReflector(new { key = passedValue }, true);

            target.Reflect(new PropertyHandlerDictionary
            {
                { "key", v => relfectedValue = (string)v }
            });

            Assert.Equal(passedValue, relfectedValue);
        }

        [Fact]
        public void ArgumentReflector_Reflect_ExplicitCaseSensitivity_UpperCaseHandlerKey_LowerCasePropName_HandlerNotRun()
        {
            var passedValue = "value";
            var relfectedValue = String.Empty;
            var target = new ArgumentReflector(new { KEY = passedValue }, true);

            target.Reflect(new PropertyHandlerDictionary
            {
                { "key", v => relfectedValue = (string)v }
            });

            Assert.NotEqual(passedValue, relfectedValue);
        }

        [Fact]
        public void ArgumentReflector_Reflect_ExplicitCaseSensitivity_LowerCaseHandlerKey_LowerCasePropName_Verify_HandlerRun_PassedPropValue()
        {
            var passedValue = "value";
            var relfectedValue = String.Empty;
            var target = new ArgumentReflector(new { key = passedValue }, true);

            target.Reflect(new PropertyHandlerDictionary
            {
                { "key", v => relfectedValue = (string)v }
            });

            Assert.Equal(passedValue, relfectedValue);
        }

        [Fact]
        public void ArgumentReflector_Reflect_UnusedHandlerKey_HandlerNotRun()
        {
            var passedValue = "value";
            var relfectedValue = String.Empty;
            var target = new ArgumentReflector(new { key = passedValue }, true);

            target.Reflect(new PropertyHandlerDictionary
            {
                { "otherkey", v => relfectedValue = "handlerRun" }
            });

            Assert.Equal(String.Empty, relfectedValue);
        }

        [Fact]
        public void ArgumentReflector_Reflect_UnusedPropertyName_HandlerNotRun()
        {
            var passedValue = "value";
            var relfectedValue = String.Empty;
            var target = new ArgumentReflector(new { otherkey = passedValue }, true);

            target.Reflect(new PropertyHandlerDictionary
            {
                { "key", v => relfectedValue = "handlerRun" }
            });

            Assert.Equal(String.Empty, relfectedValue);
        }

        [Fact]
        public void ArgumentReflector_Reflect_HandlerDictionaryBuilderNull_ThrowsException()
        {
            var target = new ArgumentReflector(new { });

            Assert.Throws<ArgumentNullException>(() =>
            {
                target.Reflect((Action<PropertyHandlerDictionaryBuilder>)null);
            });
        }

        [Fact]
        public void ArgumentReflector_Reflect_HandlerDictionaryBuilder_VerifyBuiltSetIsEffectivelyRun()
        {
            var passedValue = "value";
            var relfectedValue = String.Empty;
            var target = new ArgumentReflector(new { key = passedValue });

            target.Reflect(prop =>
            {
                prop.Named<string>("key", v => relfectedValue = v);
            });

            Assert.Equal(passedValue, relfectedValue);            
        }
    }
}
