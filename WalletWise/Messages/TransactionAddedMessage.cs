// in WalletWise/Messages/TransactionAddedMessage.cs
using CommunityToolkit.Mvvm.Messaging.Messages;
using WalletWise.Persistence.Models;

namespace WalletWise.Messages;

public class TransactionAddedMessage(Transaction value) : ValueChangedMessage<Transaction>(value)
{
}
