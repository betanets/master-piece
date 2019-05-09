using master_piece.lexeme;
using master_piece.service;
using NUnit.Framework;

namespace master_piece.tests
{
    [TestFixture]
    class ParserServiceTest
    {
        [Test]
        public void identifierTest()
        {
            string expression = "argument71a4";
            var result = ParserService.parseIfExpression(expression);

            Assert.AreEqual(result.lexemesList[0].lexemeType, LexemeType.Identifier);
            Assert.AreEqual(result.lexemesList[0].lexemeText, "argument71a4");
        }

        [Test]
        public void incorrentIdentifierTest()
        {
            string expression = "312f24wg";
            var result = ParserService.parseIfExpression(expression);

            Assert.AreEqual(result.lexemesList[0].lexemeType, LexemeType.Error);
            Assert.AreEqual(result.lexemesList[0].lexemeText, "312f24wg");
        }

        [Test]
        public void intValueTest()
        {
            string expression = "77008412";
            var result = ParserService.parseIfExpression(expression);

            Assert.AreEqual(result.lexemesList[0].lexemeType, LexemeType.IntValue);
            Assert.AreEqual(result.lexemesList[0].lexemeText, "77008412");
        }

        [Test]
        public void AnB()
        {
            string expression = "(a && b)";
            var result = ParserService.parseIfExpression(expression);

            Assert.AreEqual(result.lexemesList[0].lexemeType, LexemeType.OpenBracket);
            Assert.AreEqual(result.lexemesList[0].lexemeText, "(");

            Assert.AreEqual(result.lexemesList[1].lexemeType, LexemeType.Identifier);
            Assert.AreEqual(result.lexemesList[1].lexemeText, "a");

            Assert.AreEqual(result.lexemesList[2].lexemeType, LexemeType.And);
            Assert.AreEqual(result.lexemesList[2].lexemeText, "&&");

            Assert.AreEqual(result.lexemesList[3].lexemeType, LexemeType.Identifier);
            Assert.AreEqual(result.lexemesList[3].lexemeText, "b");

            Assert.AreEqual(result.lexemesList[4].lexemeType, LexemeType.CloseBracket);
            Assert.AreEqual(result.lexemesList[4].lexemeText, ")");
        }

        [Test]
        public void tripleOperations()
        {
            string expression = "(a1 == 1) && ((a2 == 4) || (x >= 251))";
            var result = ParserService.parseIfExpression(expression);

            Assert.AreEqual(result.lexemesList[0].lexemeType, LexemeType.OpenBracket);
            Assert.AreEqual(result.lexemesList[0].lexemeText, "(");

            Assert.AreEqual(result.lexemesList[1].lexemeType, LexemeType.Identifier);
            Assert.AreEqual(result.lexemesList[1].lexemeText, "a1");

            Assert.AreEqual(result.lexemesList[2].lexemeType, LexemeType.Equal);
            Assert.AreEqual(result.lexemesList[2].lexemeText, "==");

            Assert.AreEqual(result.lexemesList[3].lexemeType, LexemeType.IntValue);
            Assert.AreEqual(result.lexemesList[3].lexemeText, "1");

            Assert.AreEqual(result.lexemesList[4].lexemeType, LexemeType.CloseBracket);
            Assert.AreEqual(result.lexemesList[4].lexemeText, ")");

            Assert.AreEqual(result.lexemesList[5].lexemeType, LexemeType.And);
            Assert.AreEqual(result.lexemesList[5].lexemeText, "&&");

            Assert.AreEqual(result.lexemesList[6].lexemeType, LexemeType.OpenBracket);
            Assert.AreEqual(result.lexemesList[6].lexemeText, "(");

            Assert.AreEqual(result.lexemesList[7].lexemeType, LexemeType.OpenBracket);
            Assert.AreEqual(result.lexemesList[7].lexemeText, "(");

            Assert.AreEqual(result.lexemesList[8].lexemeType, LexemeType.Identifier);
            Assert.AreEqual(result.lexemesList[8].lexemeText, "a2");

            Assert.AreEqual(result.lexemesList[9].lexemeType, LexemeType.Equal);
            Assert.AreEqual(result.lexemesList[9].lexemeText, "==");

            Assert.AreEqual(result.lexemesList[10].lexemeType, LexemeType.IntValue);
            Assert.AreEqual(result.lexemesList[10].lexemeText, "4");

            Assert.AreEqual(result.lexemesList[11].lexemeType, LexemeType.CloseBracket);
            Assert.AreEqual(result.lexemesList[11].lexemeText, ")");

            Assert.AreEqual(result.lexemesList[12].lexemeType, LexemeType.Or);
            Assert.AreEqual(result.lexemesList[12].lexemeText, "||");

            Assert.AreEqual(result.lexemesList[13].lexemeType, LexemeType.OpenBracket);
            Assert.AreEqual(result.lexemesList[13].lexemeText, "(");

            Assert.AreEqual(result.lexemesList[14].lexemeType, LexemeType.Identifier);
            Assert.AreEqual(result.lexemesList[14].lexemeText, "x");

            Assert.AreEqual(result.lexemesList[15].lexemeType, LexemeType.MoreOrEqual);
            Assert.AreEqual(result.lexemesList[15].lexemeText, ">=");

            Assert.AreEqual(result.lexemesList[16].lexemeType, LexemeType.IntValue);
            Assert.AreEqual(result.lexemesList[16].lexemeText, "251");

            Assert.AreEqual(result.lexemesList[17].lexemeType, LexemeType.CloseBracket);
            Assert.AreEqual(result.lexemesList[17].lexemeText, ")");

            Assert.AreEqual(result.lexemesList[18].lexemeType, LexemeType.CloseBracket);
            Assert.AreEqual(result.lexemesList[18].lexemeText, ")");
        }

        [Test]
        public void expressionWithWhitespacesTest()
        {
            string expression = "      identifierOne         !=  ident1fier ";
            var result = ParserService.parseIfExpression(expression);

            Assert.AreEqual(result.lexemesList[0].lexemeType, LexemeType.Identifier);
            Assert.AreEqual(result.lexemesList[0].lexemeText, "identifierOne");

            Assert.AreEqual(result.lexemesList[1].lexemeType, LexemeType.NotEqual);
            Assert.AreEqual(result.lexemesList[1].lexemeText, "!=");

            Assert.AreEqual(result.lexemesList[2].lexemeType, LexemeType.Identifier);
            Assert.AreEqual(result.lexemesList[2].lexemeText, "ident1fier");
        }

        [Test]
        public void doubleFuzzyExpression()
        {
            string expression = "\"хорошо\" == \"хорошо\"";
            var result = ParserService.parseIfExpression(expression);

            Assert.AreEqual(result.lexemesList[0].lexemeType, LexemeType.FuzzyValue);
            Assert.AreEqual(result.lexemesList[0].lexemeText, "\"хорошо\"");

            Assert.AreEqual(result.lexemesList[1].lexemeType, LexemeType.Equal);
            Assert.AreEqual(result.lexemesList[1].lexemeText, "==");

            Assert.AreEqual(result.lexemesList[2].lexemeType, LexemeType.FuzzyValue);
            Assert.AreEqual(result.lexemesList[2].lexemeText, "\"хорошо\"");
        }
    }
}
