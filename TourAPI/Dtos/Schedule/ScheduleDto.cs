namespace TourAPI.Dtos.Schedule;

public class ScheduleDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DepartureDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public int Quantity { get; set; }
    public decimal PriceAdult { get; set; }
    public decimal PriceChild { get; set; }
    public string Status { get; set; }
}

// Id: Thông thường là khóa chính (primary key) của bảng, dùng để duy nhất định danh mỗi lịch trình tour.
// Name: Tên của lịch trình tour.
// DepartureDate: Ngày khởi hành của tour.
// ReturnDate: Ngày kết thúc tour.
// Quantity: Số lượng khách tham gia tour.
// PriceAdult: Giá vé cho người lớn.
// PriceChild: Giá vé cho trẻ em.
// Status: Trạng thái của lịch trình tour (ví dụ: đang mở bán, đã kín chỗ, đã hủy...).