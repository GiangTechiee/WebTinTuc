-- Thêm 20 bài viết vào bảng News
INSERT INTO [dbo].[News] ([Title], [Abstract], [Content], [PostedDate], [ViewCount], [IsApprove], [Image], [Fk_UserId])
VALUES 
    (N'Khám phá AI trong y tế', N'Tìm hiểu cách AI đang thay đổi ngành y tế với chẩn đoán thông minh.', N'Nội dung chi tiết về cách trí tuệ nhân tạo (AI) được sử dụng trong y tế, từ phân tích hình ảnh y khoa đến dự đoán bệnh lý, mang lại hiệu quả cao và tiết kiệm chi phí.', '2025-04-11 08:00:00', 45, 1, N'new1.jpg', 2),
    (N'An ninh mạng trong 2025', N'Cập nhật các mối đe dọa mạng mới nhất và cách phòng chống.', N'Bài viết phân tích các xu hướng tấn công mạng trong năm 2025 và các biện pháp bảo mật tiên tiến để bảo vệ tổ chức.', '2025-04-10 09:00:00', 32, 1, N'new2.jpg', 3),
    (N'Chuyển đổi số doanh nghiệp', N'Tìm hiểu cách doanh nghiệp áp dụng chuyển đổi số để tăng hiệu quả.', N'Bài viết thảo luận về hành trình chuyển đổi số của các doanh nghiệp Việt Nam, với các ví dụ thực tiễn và bài học kinh nghiệm.', '2025-04-09 10:00:00', 60, 1, N'new3.jpeg', 4),
    (N'Tương lai của DevOps', N'Khám phá các công cụ và phương pháp DevOps hiện đại.', N'Nội dung chi tiết về cách DevOps thay đổi cách phát triển phần mềm, với các công cụ như Docker, Kubernetes và CI/CD.', '2025-04-08 11:00:00', 25, 1, N'new4.jpeg', 2),
    (N'Ứng dụng AI trong giáo dục', N'AI đang thay đổi cách chúng ta học như thế nào?', N'Bài viết khám phá các ứng dụng AI trong giáo dục, từ hệ thống học tập cá nhân hóa đến trợ lý giảng dạy thông minh.', '2025-04-07 12:00:00', 70, 1, N'new5.jpg', 3),
    (N'Dữ liệu lớn và phân tích', N'Tận dụng dữ liệu lớn để ra quyết định thông minh.', N'Nội dung phân tích cách dữ liệu lớn được sử dụng để khám phá xu hướng và tối ưu hóa chiến lược kinh doanh.', '2025-04-06 13:00:00', 15, 1, N'new6.jpg', 4),
    (N'Đám mây và doanh nghiệp', N'Lợi ích của điện toán đám mây cho doanh nghiệp vừa và nhỏ.', N'Bài viết thảo luận về cách các doanh nghiệp sử dụng đám mây để giảm chi phí và tăng tính linh hoạt.', '2025-04-05 14:00:00', 80, 1, N'new7.jpg', 2),
    (N'IoT và thành phố thông minh', N'Công nghệ IoT đang định hình các đô thị tương lai.', N'Nội dung chi tiết về ứng dụng IoT trong quản lý giao thông, năng lượng và môi trường đô thị.', '2025-04-04 15:00:00', 50, 1, N'new8.jpg', 3),
    (N'Blockchain ngoài tiền mã hóa', N'Ứng dụng blockchain trong chuỗi cung ứng và hơn thế nữa.', N'Bài viết khám phá các ứng dụng blockchain trong quản lý dữ liệu và tăng tính minh bạch.', '2025-04-03 16:00:00', 35, 1, N'new9.jpg', 4),
    (N'Khoa học dữ liệu thực tiễn', N'Học cách áp dụng khoa học dữ liệu trong công việc.', N'Nội dung hướng dẫn từng bước về phân tích dữ liệu và xây dựng mô hình dự đoán.', '2025-04-02 17:00:00', 65, 1, N'new10.jpg', 2),
    (N'Quản lý dự án Agile', N'Làm thế nào để triển khai Agile thành công?', N'Bài viết chia sẻ các phương pháp và công cụ để quản lý dự án CNTT hiệu quả với Agile.', '2025-04-01 18:00:00', 20, 1, N'new11.jpeg', 3),
    (N'Sự kiện CNTT 2025', N'Tổng hợp các sự kiện CNTT nổi bật trong năm.', N'Nội dung cập nhật lịch trình và thông tin về các hội thảo, triển lãm công nghệ sắp tới.', '2025-03-31 19:00:00', 90, 1, N'new12.jpg', 4),
    (N'Chính sách bảo mật dữ liệu', N'Hiểu về GDPR và luật bảo mật Việt Nam.', N'Bài viết phân tích các quy định bảo mật và cách doanh nghiệp tuân thủ.', '2025-03-30 20:00:00', 40, 1, N'new13.jpg', 2),
    (N'Startup CNTT Việt Nam', N'Câu chuyện thành công của các startup công nghệ.', N'Nội dung chia sẻ kinh nghiệm và chiến lược từ các startup CNTT nổi bật.', '2025-03-29 21:00:00', 55, 1, N'new14.jpeg', 3),
    (N'Đào tạo CNTT tương lai', N'Chuẩn bị kỹ năng CNTT cho thế hệ trẻ.', N'Bài viết thảo luận về các chương trình đào tạo và chứng chỉ CNTT cần thiết.', '2025-03-28 22:00:00', 75, 1, N'new15.jpeg', 4),
    (N'Phát triển ứng dụng di động', N'Xu hướng thiết kế ứng dụng di động 2025.', N'Nội dung hướng dẫn về lập trình và tối ưu hóa UX/UI cho ứng dụng di động.', '2025-03-27 23:00:00', 30, 1, N'new16.jpg', 2),
    (N'Tự động hóa quy trình', N'RPA và tương lai của tự động hóa doanh nghiệp.', N'Bài viết khám phá cách RPA giúp tiết kiệm thời gian và tăng hiệu suất.', '2025-03-26 08:00:00', 85, 1, N'new17.jpg', 3),
    (N'Thành phố thông minh VN', N'Các dự án thành phố thông minh tại Việt Nam.', N'Nội dung chi tiết về các sáng kiến công nghệ tại Hà Nội, TP.HCM và Đà Nẵng.', '2025-03-25 09:00:00', 60, 1, N'new18.jpg', 4),
    (N'Đạo đức trong AI', N'Xây dựng AI có trách nhiệm và minh bạch.', N'Bài viết thảo luận về các vấn đề đạo đức trong phát triển và ứng dụng AI.', '2025-03-24 10:00:00', 25, 1, N'new19.jpg', 2),
    (N'Nghiên cứu CNTT mới', N'Cập nhật các nghiên cứu CNTT tiên tiến.', N'Nội dung giới thiệu các bài báo khoa học và dự án R&D nổi bật trong ngành.', '2025-03-23 11:00:00', 70, 1, N'new20.jpg', 3);

-- Thêm dữ liệu vào bảng NewsCategory
INSERT INTO [dbo].[NewsCategory] ([CategoriesCategoryId], [NewsNewId])
SELECT c.CategoryId, n.NewId
FROM [dbo].[News] n
CROSS APPLY (
    SELECT TOP 1 CategoryId
    FROM [dbo].[Categories]
    WHERE CategoryId = (
        CASE 
            WHEN EXISTS (SELECT 1 FROM [dbo].[Categories] WHERE CategoryId = (n.NewId - (SELECT MIN(NewId) FROM [dbo].[News]) + 1))
            THEN (n.NewId - (SELECT MIN(NewId) FROM [dbo].[News]) + 1)
            ELSE (SELECT MIN(CategoryId) FROM [dbo].[Categories])
        END
    )
) c
WHERE n.Image IN (N'new1.jpg', N'new2.jpg', N'new3.jpg', N'new4.jpg', N'new5.jpg', 
                  N'new6.jpg', N'new7.jpg', N'new8.jpg', N'new9.jpg', N'new10.jpg', 
                  N'new11.jpg', N'new12.jpg', N'new13.jpg', N'new14.jpg', N'new15.jpg', 
                  N'new16.jpg', N'new17.jpg', N'new18.jpg', N'new19.jpg', N'new20.jpg');