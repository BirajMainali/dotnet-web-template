using System.Transactions;

namespace App.Base.Helper
{
    public static class TransactionScopeHelper
    {
        public static TransactionScope Scope() 
            => new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
    }
}