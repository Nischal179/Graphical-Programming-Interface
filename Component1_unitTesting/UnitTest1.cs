using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Assignment1;
using System.Drawing;
using System.Security.Cryptography;
using System.IO;

namespace Component1_unitTesting
{
    [TestClass]
    public class UnitTest1
    {
        public Graphics g;
        Form1 form1 = new Form1();
        SyntaxCheck synt_chk = new SyntaxCheck();
        Var var = Var.Instance;
        If_Statement if_stat = new If_Statement();
        /// <summary>
        /// It checks the split_commands method of command parser class when invalid command gets executed in command window
        /// </summary>
        [TestMethod]
        public void Check_Invalid_Command()
        {
            StringReader sr = new StringReader("iff");
            bool actual = synt_chk.check_Syntax(sr);
            bool expected = false;
            Assert.AreEqual(expected, actual);

        }
        /// <summary>
        /// It checks the split_commands method of command parser class when valid command gets executed in command window
        /// </summary>
        [TestMethod]
        public void Check_Valid_Command()
        {
            this.g = form1.get_g();
            CommandParser cmd = new CommandParser(g);
            bool actual = cmd.split_commands("triangle 30,40");
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// It checks the split_commands method of command parser class when incomplete command e.g "drawto" with no provided parameters gets executed in command window
        /// </summary>
        [TestMethod]
        public void Check_Incomplete_Command()
        {
            this.g = form1.get_g();
            CommandParser cmd = new CommandParser(g);
            Assert.ThrowsException<InvalidCommandException>(() => cmd.split_commands("drawto"));
        }
        [TestMethod]
        public void Check_SingleLine_If_statement()
        {
            var.Add_Update_var_val(50, "a");
            Assert.IsFalse(if_stat.check_SingleLine_if("If a < 60 then ")); //Invalid Command
            Assert.IsTrue(if_stat.check_SingleLine_if("If a < 60 then rectangle 100,100")); //Valid Command
        }
        [TestMethod]
        public void Check_Multiline_if_statement()
        {
            var.Add_Update_var_val(50, "a");
            string[] valid_if = { "If a > 20", "rectangle 100,100", "endif" };
            string[] invalid_if = { "If a > 20", "rectangle 100,100" };
            Assert.IsFalse(if_stat.check_MultiLine_if(invalid_if));
            Assert.IsTrue(if_stat.check_MultiLine_if(valid_if));

        }
        [TestMethod]
        public void check_var()
        {
            //invalid variable name
            string varia = "a1 = 10";
            Assert.ThrowsException<InvalidCommandException>(() => var.Declare_var(varia));

            //valid variable decelaration
            varia = "a = 10+20";
            var.Declare_var(varia);
            int actual = var.get_variable_value("a");
            int expected = 30;
            Assert.AreEqual(expected, actual);
        }
    }
}
