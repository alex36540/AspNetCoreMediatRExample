using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace RazorPagesLab.Pages.AddressBook;

public class DeleteAddressHandler
	: IRequestHandler<DeleteAddressRequest, Guid>
{
	private readonly IRepo<AddressBookEntry> _repo;

	public DeleteAddressHandler(IRepo<AddressBookEntry> repo)
	{
		_repo = repo;
	}

	public async Task<Guid> Handle(DeleteAddressRequest request, CancellationToken cancellationToken)
	{
		// Get entry from repo and check if it exists
		var byId = new EntryByIdSpecification(request.Id);
		var entryList = _repo.Find(byId);

		if (entryList.Count == 0) 
		{
			return await Task.FromResult(Guid.Empty);
		}

        AddressBookEntry entry = entryList[0];


		_repo.Remove(entry);
		return await Task.FromResult(entry.Id);
	}
}