using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Tour;
using TourAPI.Dtos.TourSchedule;
using TourAPI.Helpers;

namespace TourAPI.Interfaces.Service
{
    public interface ITourScheduleService
    {
        Task<TourScheduleResultDto> GetByTourId(int tourId, TourScheduleQueryObject queryObject);
        Task<TourScheduleResponseDto> CreateAsync(CreateTourScheduleRequestDto requestDto);
        Task<TourScheduleResponseDto> UpdateAsync(int id, UpdateTourScheduleReqDto requestDto);
        Task<TourScheduleResponseDto> DeleteAsync(int id);
    }
}