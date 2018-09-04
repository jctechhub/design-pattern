using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.SOLID
{
    /// <summary>
    /// This is the 'I'. it means if you have big interfaces, need to break it into smaller one, so that 'you pay for things that you only need.'
    /// </summary>
    class InterfaceSegregation
    {
    }
    public class Document
    {
    }

    /// <summary>
    /// PROBLEM: Interface contains too much. 
    /// </summary>
    public interface IMachine
    {
        void Print(Document d);
        void Fax(Document d);
        void Scan(Document d);
    }

    // ok if you need a multifunction machine
    public class MultiFunctionPrinter : IMachine
    {
        public void Print(Document d)
        {
            //
        }

        public void Fax(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            //
        }
    }

    public class OldFashionedPrinter : IMachine
    {
        public void Print(Document d)
        {
            // yep
        }
        /// <summary>
        /// PROBLEM: I don't need this. 
        /// </summary>
        /// <param name="d"></param>
        public void Fax(Document d)
        {
            throw new System.NotImplementedException();
        }

        public void Scan(Document d)
        {
            throw new System.NotImplementedException();
        }
    }

    ///=========================
    /// <summary>
    /// Solution: build small interfaces
    /// </summary>
    public interface IPrinter
    {
        void Print(Document d);
    }

    public interface IScanner
    {
        void Scan(Document d);
    }

    //only implements the one needed
    public class Printer : IPrinter
    {
        public void Print(Document d)
        {

        }
    }

    public class Photocopier : IPrinter, IScanner
    {
        public void Print(Document d)
        {
            throw new System.NotImplementedException();
        }

        public void Scan(Document d)
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// INTERESTING: Interface wrap within interface
    /// </summary>
    public interface IMultiFunctionDevice : IPrinter, IScanner //
    {

    }

    /// <summary>
    /// More interesting: this is the DECORATOR pattern. 
    /// delegate the interface implementation to the objects inside the class. 
    /// this doens't directly implment the interface. 
    /// </summary>
    public struct MultiFunctionMachine : IMultiFunctionDevice
    {
        // compose this out of several modules
        private IPrinter printer;
        private IScanner scanner;

        public MultiFunctionMachine(IPrinter printer, IScanner scanner)
        {
            if (printer == null)
            {
                throw new ArgumentNullException(paramName: nameof(printer));
            }
            if (scanner == null)
            {
                throw new ArgumentNullException(paramName: nameof(scanner));
            }
            this.printer = printer;
            this.scanner = scanner;
        }

        public void Print(Document d)
        {
            printer.Print(d);
        }

        public void Scan(Document d)
        {
            scanner.Scan(d);
        }
    }

}
