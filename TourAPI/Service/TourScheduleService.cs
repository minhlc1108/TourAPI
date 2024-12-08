using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Tour;
using TourAPI.Dtos.TourSchedule;
using TourAPI.Exceptions;
using TourAPI.Helpers;
using TourAPI.Interfaces.Repository;
using TourAPI.Interfaces.Service;
using TourAPI.Mappers;

namespace TourAPI.Service
{
    public class TourScheduleService : ITourScheduleService
    {
        private readonly ITourScheduleRepository _tourScheduleRepository;
        private readonly ITourRepository _tourRepository;

        public TourScheduleService(ITourScheduleRepository tourScheduleRepository, ITourRepository tourRepository)
        {
            _tourScheduleRepository = tourScheduleRepository;
            _tourRepository = tourRepository;
        }

        public async Task<TourScheduleResponseDto> CreateAsync(CreateTourScheduleRequestDto requestDto)
        {
            var tour = await _tourRepository.GetByIdAsync(requestDto.TourId);
            if (tour == null)
            {
                throw new NotFoundException("Không tìm thấy tour");
            }

            if (tour.TourSchedules.Any(ts => ts.DepartureDate == requestDto.DepartureDate))
            {
                throw new InvalidOperationException("Ngày này đã có lịch khởi hành!");
            }

            var tourSchedule = requestDto.ToTourScheduleFromCreateDTO(tour.Quantity, tour.Duration);

            var createdTourSchedule = await _tourScheduleRepository.CreateAsync(tourSchedule);
            return createdTourSchedule.ToTourScheduleResponseDto();
        }

        public async Task<TourScheduleResponseDto> DeleteAsync(int id)
        {
            var tourSchedule = await _tourScheduleRepository.DeleteAsync(id);
            if (tourSchedule == null)
            {
                throw new NotFoundException("Không tìm thấy lịch khởi hành");
            }
            return tourSchedule.ToTourScheduleResponseDto();
        }

        public async Task<TourScheduleDto?> GetByIdAsync(int id)
        {
            var tourSchedule = await _tourScheduleRepository.GetByIdAsync(id);
            if(tourSchedule == null) {
                throw new NotFoundException("Không tìm thấy lịch khởi hành");
            }
            if(tourSchedule.DepartureDate < DateTime.Now) {
                throw new InvalidOperationException("Không thể xem thông tin lịch khởi hành đã qua");
            }
            return tourSchedule.ToTourScheduleDto();
        }

        public async Task<TourScheduleResultDto> GetTourSchedules(TourScheduleQueryObject queryObject)
        {
            var (tourSchedules, total) = await _tourScheduleRepository.GetTourSchedules(queryObject);
            return new TourScheduleResultDto
            {
                TourSchedules = tourSchedules.Select(t => t.ToTourScheduleResponseDto()).ToList(),
                Total = total
            };
        }

        public async Task<TourScheduleResponseDto> UpdateAsync(int id, UpdateTourScheduleReqDto requestDto)
        {
            var tourSchedule = await _tourScheduleRepository.GetByIdAsync(id);
            if (tourSchedule == null)
            {
                throw new NotFoundException("Không tìm thấy lịch khởi hành");
            }

            tourSchedule.Name = requestDto.Name;
            tourSchedule.PriceAdult = requestDto.PriceAdult;
            tourSchedule.PriceChild = requestDto.PriceChild;

            await _tourScheduleRepository.UpdateAsync(tourSchedule);
            return tourSchedule.ToTourScheduleResponseDto();
        }
    }
}