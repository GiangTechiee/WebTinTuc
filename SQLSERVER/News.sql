-- Th�m 20 b�i vi?t v�o b?ng News
INSERT INTO [dbo].[News] ([Title], [Abstract], [Content], [PostedDate], [ViewCount], [IsApprove], [Image], [Fk_UserId])
VALUES 
    (N'Kh�m ph� AI trong y t?', N'T�m hi?u c�ch AI ?ang thay ??i ng�nh y t? v?i ch?n ?o�n th�ng minh.', N'N?i dung chi ti?t v? c�ch tr� tu? nh�n t?o (AI) ???c s? d?ng trong y t?, t? ph�n t�ch h�nh ?nh y khoa ??n d? ?o�n b?nh l�, mang l?i hi?u qu? cao v� ti?t ki?m chi ph�.', '2025-04-11 08:00:00', 45, 1, N'new1.jpg', 2),
    (N'An ninh m?ng trong 2025', N'C?p nh?t c�c m?i ?e d?a m?ng m?i nh?t v� c�ch ph�ng ch?ng.', N'B�i vi?t ph�n t�ch c�c xu h??ng t?n c�ng m?ng trong n?m 2025 v� c�c bi?n ph�p b?o m?t ti�n ti?n ?? b?o v? t? ch?c.', '2025-04-10 09:00:00', 32, 1, N'new2.jpg', 3),
    (N'Chuy?n ??i s? doanh nghi?p', N'T�m hi?u c�ch doanh nghi?p �p d?ng chuy?n ??i s? ?? t?ng hi?u qu?.', N'B�i vi?t th?o lu?n v? h�nh tr�nh chuy?n ??i s? c?a c�c doanh nghi?p Vi?t Nam, v?i c�c v� d? th?c ti?n v� b�i h?c kinh nghi?m.', '2025-04-09 10:00:00', 60, 1, N'new3.jpg', 4),
    (N'T??ng lai c?a DevOps', N'Kh�m ph� c�c c�ng c? v� ph??ng ph�p DevOps hi?n ??i.', N'N?i dung chi ti?t v? c�ch DevOps thay ??i c�ch ph�t tri?n ph?n m?m, v?i c�c c�ng c? nh? Docker, Kubernetes v� CI/CD.', '2025-04-08 11:00:00', 25, 1, N'new4.jpg', 2),
    (N'?ng d?ng AI trong gi�o d?c', N'AI ?ang thay ??i c�ch ch�ng ta h?c nh? th? n�o?', N'B�i vi?t kh�m ph� c�c ?ng d?ng AI trong gi�o d?c, t? h? th?ng h?c t?p c� nh�n h�a ??n tr? l� gi?ng d?y th�ng minh.', '2025-04-07 12:00:00', 70, 1, N'new5.jpg', 3),
    (N'D? li?u l?n v� ph�n t�ch', N'T?n d?ng d? li?u l?n ?? ra quy?t ??nh th�ng minh.', N'N?i dung ph�n t�ch c�ch d? li?u l?n ???c s? d?ng ?? kh�m ph� xu h??ng v� t?i ?u h�a chi?n l??c kinh doanh.', '2025-04-06 13:00:00', 15, 1, N'new6.jpg', 4),
    (N'?�m m�y v� doanh nghi?p', N'L?i �ch c?a ?i?n to�n ?�m m�y cho doanh nghi?p v?a v� nh?.', N'B�i vi?t th?o lu?n v? c�ch c�c doanh nghi?p s? d?ng ?�m m�y ?? gi?m chi ph� v� t?ng t�nh linh ho?t.', '2025-04-05 14:00:00', 80, 1, N'new7.jpg', 2),
    (N'IoT v� th�nh ph? th�ng minh', N'C�ng ngh? IoT ?ang ??nh h�nh c�c ?� th? t??ng lai.', N'N?i dung chi ti?t v? ?ng d?ng IoT trong qu?n l� giao th�ng, n?ng l??ng v� m�i tr??ng ?� th?.', '2025-04-04 15:00:00', 50, 1, N'new8.jpg', 3),
    (N'Blockchain ngo�i ti?n m� h�a', N'?ng d?ng blockchain trong chu?i cung ?ng v� h?n th? n?a.', N'B�i vi?t kh�m ph� c�c ?ng d?ng blockchain trong qu?n l� d? li?u v� t?ng t�nh minh b?ch.', '2025-04-03 16:00:00', 35, 1, N'new9.jpg', 4),
    (N'Khoa h?c d? li?u th?c ti?n', N'H?c c�ch �p d?ng khoa h?c d? li?u trong c�ng vi?c.', N'N?i dung h??ng d?n t?ng b??c v? ph�n t�ch d? li?u v� x�y d?ng m� h�nh d? ?o�n.', '2025-04-02 17:00:00', 65, 1, N'new10.jpg', 2),
    (N'Qu?n l� d? �n Agile', N'L�m th? n�o ?? tri?n khai Agile th�nh c�ng?', N'B�i vi?t chia s? c�c ph??ng ph�p v� c�ng c? ?? qu?n l� d? �n CNTT hi?u qu? v?i Agile.', '2025-04-01 18:00:00', 20, 1, N'new11.jpg', 3),
    (N'S? ki?n CNTT 2025', N'T?ng h?p c�c s? ki?n CNTT n?i b?t trong n?m.', N'N?i dung c?p nh?t l?ch tr�nh v� th�ng tin v? c�c h?i th?o, tri?n l�m c�ng ngh? s?p t?i.', '2025-03-31 19:00:00', 90, 1, N'new12.jpg', 4),
    (N'Ch�nh s�ch b?o m?t d? li?u', N'Hi?u v? GDPR v� lu?t b?o m?t Vi?t Nam.', N'B�i vi?t ph�n t�ch c�c quy ??nh b?o m?t v� c�ch doanh nghi?p tu�n th?.', '2025-03-30 20:00:00', 40, 1, N'new13.jpg', 2),
    (N'Startup CNTT Vi?t Nam', N'C�u chuy?n th�nh c�ng c?a c�c startup c�ng ngh?.', N'N?i dung chia s? kinh nghi?m v� chi?n l??c t? c�c startup CNTT n?i b?t.', '2025-03-29 21:00:00', 55, 1, N'new14.jpg', 3),
    (N'?�o t?o CNTT t??ng lai', N'Chu?n b? k? n?ng CNTT cho th? h? tr?.', N'B�i vi?t th?o lu?n v? c�c ch??ng tr�nh ?�o t?o v� ch?ng ch? CNTT c?n thi?t.', '2025-03-28 22:00:00', 75, 1, N'new15.jpg', 4),
    (N'Ph�t tri?n ?ng d?ng di ??ng', N'Xu h??ng thi?t k? ?ng d?ng di ??ng 2025.', N'N?i dung h??ng d?n v? l?p tr�nh v� t?i ?u h�a UX/UI cho ?ng d?ng di ??ng.', '2025-03-27 23:00:00', 30, 1, N'new16.jpg', 2),
    (N'T? ??ng h�a quy tr�nh', N'RPA v� t??ng lai c?a t? ??ng h�a doanh nghi?p.', N'B�i vi?t kh�m ph� c�ch RPA gi�p ti?t ki?m th?i gian v� t?ng hi?u su?t.', '2025-03-26 08:00:00', 85, 1, N'new17.jpg', 3),
    (N'Th�nh ph? th�ng minh VN', N'C�c d? �n th�nh ph? th�ng minh t?i Vi?t Nam.', N'N?i dung chi ti?t v? c�c s�ng ki?n c�ng ngh? t?i H� N?i, TP.HCM v� ?� N?ng.', '2025-03-25 09:00:00', 60, 1, N'new18.jpg', 4),
    (N'??o ??c trong AI', N'X�y d?ng AI c� tr�ch nhi?m v� minh b?ch.', N'B�i vi?t th?o lu?n v? c�c v?n ?? ??o ??c trong ph�t tri?n v� ?ng d?ng AI.', '2025-03-24 10:00:00', 25, 1, N'new19.jpg', 2),
    (N'Nghi�n c?u CNTT m?i', N'C?p nh?t c�c nghi�n c?u CNTT ti�n ti?n.', N'N?i dung gi?i thi?u c�c b�i b�o khoa h?c v� d? �n R&D n?i b?t trong ng�nh.', '2025-03-23 11:00:00', 70, 1, N'new20.jpg', 3);

-- Th�m d? li?u v�o b?ng NewsCategory
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