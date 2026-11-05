-- =========================================================================
-- HỌ TÊN SINH VIÊN: NGUYỄN TUẤN ANH
-- MÔ TẢ: TOÀN BỘ SCRIPT TẠO STORED PROCEDURE BUỔI 5 (TIẾT 3: CỐT LÕI + MỞ RỘNG)
-- CSDL: NguyenTuanAnh_MiniSupperMakert
-- =========================================================================

USE NguyenTuanAnh_MiniSupperMakert;
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
        ISNULL(SUM(TotalAmount), 0) AS TotalRevenue,
        0 AS TotalPointsAwarded
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
        ISNULL(SUM(TotalAmount), 0) AS TotalRevenue,
        0 AS TotalPointsAwarded
    FROM Orders
    WHERE OrderDate >= @FromDate AND OrderDate <= @ToDate
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
        ISNULL(SUM(d.Quantity), 0) AS TotalSoldQuantity,
        ISNULL(SUM(d.Quantity * d.UnitPrice), 0) AS TotalRevenue
    FROM OrderDetails d
    INNER JOIN Products p ON d.ProductId = p.ProductId
    GROUP BY p.ProductId, p.Barcode, p.ProductName
    ORDER BY TotalSoldQuantity DESC;
END
GO

-- =========================================================================
-- 4. TIÊU CHÍ 45: sp_GetDailyRevenueGroupedByDate
-- Thống kê doanh thu theo từng ngày gom nhóm GROUP BY CAST(OrderDate AS DATE)
-- =========================================================================
IF OBJECT_ID('sp_GetDailyRevenueGroupedByDate', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetDailyRevenueGroupedByDate;
GO

CREATE PROCEDURE sp_GetDailyRevenueGroupedByDate
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        CAST(OrderDate AS DATE) AS [Date],
        COUNT(OrderId) AS TotalOrders,
        ISNULL(SUM(TotalAmount), 0) AS TotalRevenue
    FROM Orders
    WHERE OrderDate >= @FromDate AND OrderDate <= @ToDate
    GROUP BY CAST(OrderDate AS DATE)
    ORDER BY [Date] ASC;
END
GO
