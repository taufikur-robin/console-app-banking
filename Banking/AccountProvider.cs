using Banking.Data;
using Banking.Models;

namespace Banking;

public class AccountProvider
{
    private readonly BankingContext _context;

    public AccountModel GetAccountModel()
    {
        var _accountModel = _context.Accounts.FirstOrDefault() ?? new AccountModel();
        if (_accountModel.Id == 0)
        {
            _context.Accounts.Add(_accountModel);
            _context.SaveChanges();
        }
        return _accountModel;
    }

    public AccountProvider(BankingContext context)
    {
        _context = context;
    }
}