using System;
using Machine.Specifications;
using NUnit.Framework;

namespace StringCalculator.Tests {
    [TestFixture]
    public class CalculatorTests {
        [SetUp]
        public void Setup() {
            calculator = new Calculator();
        }

        private Calculator calculator;

        [Test]
        public void When_Five_numbers_are_passed_in_seperated_by_comma() {
            calculator.Add("1,2,3,4,5").ShouldEqual(15);
        }

        [Test]
        public void When_a_custom_delimiter_is_specified() {
            calculator.Add("//;\n1;2").ShouldEqual(3);
        }

        [Test]
        public void When_empty_string_return_zero() {
            calculator.Add("").ShouldEqual(0);
        }

        [Test]
        public void When_negative_numbers_are_passed_in_an_error_is_thrown() {
            Assert.Throws<Exception>(() => calculator.Add("-1,2"), "Negative numbers are not allows. The negative numbers are -1");
        }

        [Test]
        public void When_numbers_are_passed_in_seperated_by_comma_With_NewLine() {
            calculator.Add("1\n2,3").ShouldEqual(6);
        }

        [Test]
        public void When_one_number_is_greater_than_1000_should_be_ignored() {
            calculator.Add("1001, 2").ShouldEqual(2);
        }

        [Test]
        public void When_the_number_one_is_passed_in() {
            calculator.Add("1").ShouldEqual(1);
        }

        [Test]
        public void When_the_number_two_is_passed_in() {
            calculator.Add("2").ShouldEqual(2);
        }

        [Test]
        public void When_two_numbers_are_passed_in_seperated_by_comma() {
            calculator.Add("2,2").ShouldEqual(4);
        }

        [Test]
        public void Should_Support_Different_Delimiters_Length() {
            calculator.Add("//[***]\n0***1***2").ShouldEqual(3);
            //calculator.Add("//;\n1;2").ShouldEqual(3);
        }
    }
}