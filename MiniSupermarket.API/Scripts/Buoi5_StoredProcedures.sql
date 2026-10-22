-- =========================================================================
-- HỌ TÊN SINH VIÊN: NGUYỄN TUẤN ANH
-- MÔ TẢ: TOÀN BỘ SCRIPT TẠO STORED PROCEDURE BUỔI 5 (TIẾT 3: CỐT LÕI + MỞ RỘNG)
-- CSDL: MiniSupermarketDb (hoặc CSDL chỉ định)
-- =========================================================================

USE MiniSupermarketDb;
GO

-- =========================================================================
-- 1. STORED PROCEDURE TIẾT 3: sp_GetRevenueByCashier
-- Thống kê tổng hợp doanh thu, tổng số đơn và điểm thưởng theo thu ngân
-- =========================================================================
IF OBJECT_ID('sp_GetRevenueByCashier', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetRevenueByCashier;
GO

CREATE PROCEDURE sp_GetRevenueByCashier
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        CashierUsername,
        COUNT(OrderId) AS TotalOrders,
        SUM(FinalAmount) AS TotalRevenue,
        SUM(RewardPoints) AS TotalPointsAwarded
    FROM Orders
    GROUP BY CashierUsername
    ORDER BY TotalRevenue DESC;
END
GO

-- =========================================================================
-- 2. BÀI TẬP MỞ RỘNG 3.1: sp_GetRevenueByDateRange
-- Thống kê doanh thu theo khoảng thời gian có tham số @FromDate và @ToDate
-- =========================================================================
IF OBJECT_ID('sp_GetRevenueByDateRange', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetRevenueByDateRange;
GO

CREATE PROCEDURE sp_GetRevenueByDateRange
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        CashierUsername,
        COUNT(OrderId) AS TotalOrders,
        SUM(FinalAmount) AS TotalRevenue,
        SUM(RewardPoints) AS TotalPointsAwarded
    FROM Orders
    WHERE CreatedDate >= @FromDate AND CreatedDate <= @ToDate
    GROUP BY CashierUsername
    ORDER BY TotalRevenue DESC;
END
GO

-- =========================================================================
-- 3. BÀI TẬP MỞ RỘNG 3.2: sp_GetTop5SellingProducts
-- Thống kê Top 5 sản phẩm có số lượng bán nhiều nhất phục vụ Dashboard Admin
-- =========================================================================
IF OBJECT_ID('sp_GetTop5SellingProducts', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetTop5SellingProducts;
GO

CREATE PROCEDURE sp_GetTop5SellingProducts
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 5
        p.ProductId,
        p.Barcode,
        p.ProductName,
        SUM(d.Quantity) AS TotalSoldQuantity,
        SUM(d.LineTotal) AS TotalRevenue
    FROM OrderDetails d
    INNER JOIN Products p ON d.ProductId = p.ProductId
    GROUP BY p.ProductId, p.Barcode, p.ProductName
    ORDER BY TotalSoldQuantity DESC;
END
GO

-- =========================================================================
-- CÁC CÂU LỆNH KIỂM TRA THỬ NGHIỆM TRÊN SSMS:
-- =========================================================================
-- EXEC sp_GetRevenueByCashier;
-- EXEC sp_GetRevenueByDateRange '2026-08-01 00:00:00', '2026-08-31 23:59:59';
-- EXEC sp_GetTop5SellingProducts;
