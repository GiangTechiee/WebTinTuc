-- Thêm 20 bài vi?t vào b?ng News
INSERT INTO [dbo].[News] ([Title], [Abstract], [Content], [PostedDate], [ViewCount], [IsApprove], [Image], [Fk_UserId])
VALUES 
    (N'Khám phá AI trong y t?', N'Tìm hi?u cách AI ?ang thay ??i ngành y t? v?i ch?n ?oán thông minh.', N'N?i dung chi ti?t v? cách trí tu? nhân t?o (AI) ???c s? d?ng trong y t?, t? phân tích hình ?nh y khoa ??n d? ?oán b?nh lý, mang l?i hi?u qu? cao và ti?t ki?m chi phí.', '2025-04-11 08:00:00', 45, 1, N'new1.jpg', 2),
    (N'An ninh m?ng trong 2025', N'C?p nh?t các m?i ?e d?a m?ng m?i nh?t và cách phòng ch?ng.', N'Bài vi?t phân tích các xu h??ng t?n công m?ng trong n?m 2025 và các bi?n pháp b?o m?t tiên ti?n ?? b?o v? t? ch?c.', '2025-04-10 09:00:00', 32, 1, N'new2.jpg', 3),
    (N'Chuy?n ??i s? doanh nghi?p', N'Tìm hi?u cách doanh nghi?p áp d?ng chuy?n ??i s? ?? t?ng hi?u qu?.', N'Bài vi?t th?o lu?n v? hành trình chuy?n ??i s? c?a các doanh nghi?p Vi?t Nam, v?i các ví d? th?c ti?n và bài h?c kinh nghi?m.', '2025-04-09 10:00:00', 60, 1, N'new3.jpg', 4),
    (N'T??ng lai c?a DevOps', N'Khám phá các công c? và ph??ng pháp DevOps hi?n ??i.', N'N?i dung chi ti?t v? cách DevOps thay ??i cách phát tri?n ph?n m?m, v?i các công c? nh? Docker, Kubernetes và CI/CD.', '2025-04-08 11:00:00', 25, 1, N'new4.jpg', 2),
    (N'?ng d?ng AI trong giáo d?c', N'AI ?ang thay ??i cách chúng ta h?c nh? th? nào?', N'Bài vi?t khám phá các ?ng d?ng AI trong giáo d?c, t? h? th?ng h?c t?p cá nhân hóa ??n tr? lý gi?ng d?y thông minh.', '2025-04-07 12:00:00', 70, 1, N'new5.jpg', 3),
    (N'D? li?u l?n và phân tích', N'T?n d?ng d? li?u l?n ?? ra quy?t ??nh thông minh.', N'N?i dung phân tích cách d? li?u l?n ???c s? d?ng ?? khám phá xu h??ng và t?i ?u hóa chi?n l??c kinh doanh.', '2025-04-06 13:00:00', 15, 1, N'new6.jpg', 4),
    (N'?ám mây và doanh nghi?p', N'L?i ích c?a ?i?n toán ?ám mây cho doanh nghi?p v?a và nh?.', N'Bài vi?t th?o lu?n v? cách các doanh nghi?p s? d?ng ?ám mây ?? gi?m chi phí và t?ng tính linh ho?t.', '2025-04-05 14:00:00', 80, 1, N'new7.jpg', 2),
    (N'IoT và thành ph? thông minh', N'Công ngh? IoT ?ang ??nh hình các ?ô th? t??ng lai.', N'N?i dung chi ti?t v? ?ng d?ng IoT trong qu?n lý giao thông, n?ng l??ng và môi tr??ng ?ô th?.', '2025-04-04 15:00:00', 50, 1, N'new8.jpg', 3),
    (N'Blockchain ngoài ti?n mã hóa', N'?ng d?ng blockchain trong chu?i cung ?ng và h?n th? n?a.', N'Bài vi?t khám phá các ?ng d?ng blockchain trong qu?n lý d? li?u và t?ng tính minh b?ch.', '2025-04-03 16:00:00', 35, 1, N'new9.jpg', 4),
    (N'Khoa h?c d? li?u th?c ti?n', N'H?c cách áp d?ng khoa h?c d? li?u trong công vi?c.', N'N?i dung h??ng d?n t?ng b??c v? phân tích d? li?u và xây d?ng mô hình d? ?oán.', '2025-04-02 17:00:00', 65, 1, N'new10.jpg', 2),
    (N'Qu?n lý d? án Agile', N'Làm th? nào ?? tri?n khai Agile thành công?', N'Bài vi?t chia s? các ph??ng pháp và công c? ?? qu?n lý d? án CNTT hi?u qu? v?i Agile.', '2025-04-01 18:00:00', 20, 1, N'new11.jpg', 3),
    (N'S? ki?n CNTT 2025', N'T?ng h?p các s? ki?n CNTT n?i b?t trong n?m.', N'N?i dung c?p nh?t l?ch trình và thông tin v? các h?i th?o, tri?n lãm công ngh? s?p t?i.', '2025-03-31 19:00:00', 90, 1, N'new12.jpg', 4),
    (N'Chính sách b?o m?t d? li?u', N'Hi?u v? GDPR và lu?t b?o m?t Vi?t Nam.', N'Bài vi?t phân tích các quy ??nh b?o m?t và cách doanh nghi?p tuân th?.', '2025-03-30 20:00:00', 40, 1, N'new13.jpg', 2),
    (N'Startup CNTT Vi?t Nam', N'Câu chuy?n thành công c?a các startup công ngh?.', N'N?i dung chia s? kinh nghi?m và chi?n l??c t? các startup CNTT n?i b?t.', '2025-03-29 21:00:00', 55, 1, N'new14.jpg', 3),
    (N'?ào t?o CNTT t??ng lai', N'Chu?n b? k? n?ng CNTT cho th? h? tr?.', N'Bài vi?t th?o lu?n v? các ch??ng trình ?ào t?o và ch?ng ch? CNTT c?n thi?t.', '2025-03-28 22:00:00', 75, 1, N'new15.jpg', 4),
    (N'Phát tri?n ?ng d?ng di ??ng', N'Xu h??ng thi?t k? ?ng d?ng di ??ng 2025.', N'N?i dung h??ng d?n v? l?p trình và t?i ?u hóa UX/UI cho ?ng d?ng di ??ng.', '2025-03-27 23:00:00', 30, 1, N'new16.jpg', 2),
    (N'T? ??ng hóa quy trình', N'RPA và t??ng lai c?a t? ??ng hóa doanh nghi?p.', N'Bài vi?t khám phá cách RPA giúp ti?t ki?m th?i gian và t?ng hi?u su?t.', '2025-03-26 08:00:00', 85, 1, N'new17.jpg', 3),
    (N'Thành ph? thông minh VN', N'Các d? án thành ph? thông minh t?i Vi?t Nam.', N'N?i dung chi ti?t v? các sáng ki?n công ngh? t?i Hà N?i, TP.HCM và ?à N?ng.', '2025-03-25 09:00:00', 60, 1, N'new18.jpg', 4),
    (N'??o ??c trong AI', N'Xây d?ng AI có trách nhi?m và minh b?ch.', N'Bài vi?t th?o lu?n v? các v?n ?? ??o ??c trong phát tri?n và ?ng d?ng AI.', '2025-03-24 10:00:00', 25, 1, N'new19.jpg', 2),
    (N'Nghiên c?u CNTT m?i', N'C?p nh?t các nghiên c?u CNTT tiên ti?n.', N'N?i dung gi?i thi?u các bài báo khoa h?c và d? án R&D n?i b?t trong ngành.', '2025-03-23 11:00:00', 70, 1, N'new20.jpg', 3);

-- Thêm d? li?u vào b?ng NewsCategory
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