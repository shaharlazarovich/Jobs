namespace Domain {
    public class PaginatedRespone<T> {
        public int page {get; set;}
        public int totalPages {get; set;}
        public int pageSize {get; set;}
        public T records {get; set;}
    }
}