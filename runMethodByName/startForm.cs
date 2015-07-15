using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace runMethodByName
{
    public partial class startForm : Form
    {
        public startForm()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            Invoker.CreateAndInvoke(txtClassName.Text, null, txtMethodName.Text, null);

            Invoker.CreateAndInvoke(txtClassName.Text, new[] { txtCp.Text }, "TestMethodUsingValueFromConstructorAndArgs", new object[] { txtArgs.Text, false });
        }
    }

    public static class Invoker
    {
        public static object CreateAndInvoke(string typeName, object[] constructorArgs, string methodName, object[] methodArgs)
        {
            try
            {
                Type type = Type.GetType(typeName);
                object instance = Activator.CreateInstance(type, constructorArgs);

                MethodInfo method = type.GetMethod(methodName);
                return method.Invoke(instance, methodArgs);
            }
            catch (Exception Err)
            {
                MessageBox.Show(@"Method is not exists");
                return null;
            }
        }
    }


    public class Tester
    {
        public string _testField;

        public Tester()
        {
        }

        public Tester(string arg)
        {
            _testField = arg;
        }

        public void TestMethod()
        {
            MessageBox.Show("Called TestMethod");
        }

        public void TestMethodWithArg(string arg)
        {
            MessageBox.Show("Called TestMethodWithArg: " + arg);
        }

        public void TestMethodUsingValueFromConstructorAndArgs(string arg, bool arg2)
        {
            MessageBox.Show("Called TestMethodUsingValueFromConstructorAndArg " + arg + " " + arg2 + " " + _testField);
        }

        public string GetContstructorValue()
        {
            return _testField;
        }
    }
}
