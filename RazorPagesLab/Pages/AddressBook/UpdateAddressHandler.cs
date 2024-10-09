using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace RazorPagesLab.Pages.AddressBook;

public class UpdateAddressHandler
	: IRequestHandler<UpdateAddressRequest, Guid>
{
	private readonly IRepo<AddressBookEntry> _repo;

	public UpdateAddressHandler(IRepo<AddressBookEntry> repo)
	{
		_repo = repo;
	}

	public async Task<Guid> Handle(UpdateAddressRequest request, CancellationToken cancellationToken)
	{
        // Get entry from repo and check if it exists
		var byId = new EntryByIdSpecification(request.Id);
		var entryList = _repo.Find(byId);

		if (entryList.Count == 0) 
		{
			return Guid.Empty;
		}

        AddressBookEntry entry = entryList[0];
        
        // Update fields from request
        if (request.Id == entry.Id) 
        {
            entry.Update(
                request.Line1,
                request.Line2,
                request.City,
                request.State,
                request.PostalCode
            );
        }

		return await Task.FromResult(entry.Id);
	}
}