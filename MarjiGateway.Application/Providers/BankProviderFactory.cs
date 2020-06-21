using System;
using System.Collections;
using System.Collections.Generic;
using MarjiGateway.Application.Ports;

namespace MarjiGateway.Application.Providers
{
    public class BankProviderFactory : IBankProviderFactory
    {
        private readonly IDictionary<string, IBankAdapter> _banks;

        public BankProviderFactory(IDictionary<string, IBankAdapter> banks)
        {
            _banks = banks;
        }
        public IBankAdapter Create(string bankProvider)
        {
            if (!_banks.ContainsKey(bankProvider))
            {
                throw new ArgumentException(
                    $"BankProvider {bankProvider} not supported.");
            }
            return _banks[bankProvider];
        }
    }
}