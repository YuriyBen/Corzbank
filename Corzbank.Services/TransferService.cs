using AutoMapper;
using Corzbank.Data.Entities;
using Corzbank.Data.Entities.Models;
using Corzbank.Data.Enums;
using Corzbank.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services
{
    public class TransferService : ITransferService
    {
        private readonly GenericService<Transfer> _genericService;
        private readonly IMapper _mapper;

        public TransferService(GenericService<Transfer> genericService, IMapper mapper)
        {
            _genericService = genericService;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Transfer>> GetTransfers()
        {
            return await _genericService.GetRange();
        }

        public async Task<Transfer> GetTransferById(Guid id)
        {
            return await _genericService.Get(id);
        }

        public async Task<Transfer> CreateTransfer(TransferModel transferRequest)
        {
            if (transferRequest.TransferType == TransferType.Card)
            {
                transferRequest.ReceiverPhoneNumber = null;

                if (transferRequest.ReceiverCardId == null)
                    return null;
            }
            else
            {
                transferRequest.ReceiverCardId = null;

                if (transferRequest.ReceiverPhoneNumber == null)
                    return null;
            }

            var transfer = _mapper.Map<Transfer>(transferRequest);

            await _genericService.Insert(transfer);

            return transfer;
        }

        public async Task<bool> DeleteTransfer(Guid id)
        {
            var transfer = await GetTransferById(id);

            if (transfer != null)
            {
                await _genericService.Remove(transfer);

                return true;
            }
            return false;
        }
    }
}
