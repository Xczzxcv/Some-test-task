using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
internal class FlexibleGridLayout : LayoutGroup
{
    [SerializeField] private GridFitType fitType;
    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private Vector2 cellSize;
    [SerializeField, Tooltip("Working when 'fitX' and 'fitY' params are both false and aspectRatio value is not default")] 
    private float aspectRatio;
    [SerializeField] private bool fitX;
    [SerializeField] private bool fitY;
    [SerializeField] private Vector2 spacing;

    internal enum GridFitType
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumns
    }

    public GridFitType FitType => fitType;
    public int Rows => rows;
    public int Columns => columns;
    public Vector2 CellSize => cellSize;
    public bool FitX => fitX;
    public bool FitY => fitY;
    public Vector2 Spacing => spacing;

    public void Setup(int rowsAmount, int columnsAmount, GridFitType gridFitType)
    {
        rows = rowsAmount;
        columns = columnsAmount;
        fitType = gridFitType;
        SetDirty();
    }

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        SetupRowsAndColumns();
        var calculatedCellSize = CalculateCellSize();
        UpdateChildren(calculatedCellSize);
    }

    private void SetupRowsAndColumns()
    {
        var approxSize = Mathf.Sqrt(transform.childCount);
        rows = Mathf.Max(1, rows);
        columns = Mathf.Max(1, columns);

        switch (fitType)
        {
            case GridFitType.Uniform:
                rows = Mathf.CeilToInt(approxSize);
                columns = Mathf.CeilToInt(approxSize);
                break;
            case GridFitType.Width:
                rows = Mathf.CeilToInt(transform.childCount / (float) columns);
                columns = Mathf.CeilToInt(approxSize);
                break;
            case GridFitType.Height:
                rows = Mathf.CeilToInt(transform.childCount / (float) rows);
                columns = Mathf.CeilToInt(approxSize);
                break;
            case GridFitType.FixedRows:
                columns = Mathf.CeilToInt(transform.childCount / (float) rows);
                break;
            case GridFitType.FixedColumns:
                rows = Mathf.CeilToInt(transform.childCount / (float) columns);
                break;
            default:
                throw new ArgumentOutOfRangeException($"{gameObject} : {fitType}");
        }
    }

    private Vector2 CalculateCellSize()
    {
        var parentSize = new Vector2(
            rectTransform.rect.width,
            rectTransform.rect.height
        );
        var spacingTotalSize = new Vector2(
            spacing.x * (columns - 1),
            spacing.y * (rows - 1)
        );
        var paddingTotalSize = new Vector2(
            padding.left + padding.right,
            padding.top + padding.bottom
        );

        float cellWidth;
        float cellHeight;
        var calculatedCellWidth = (parentSize.x - spacingTotalSize.x - paddingTotalSize.x) / columns;
        var calculatedCellHeight = (parentSize.y - spacingTotalSize.y - paddingTotalSize.y) / rows;
        if (!fitX && !fitY && aspectRatio != default)
        {
            if (!TryFitCellByWidth(calculatedCellWidth, calculatedCellHeight, out cellWidth, out cellHeight))
            {
                if (!TryFitCellByHeight(calculatedCellWidth, calculatedCellHeight, out cellWidth, out cellHeight))
                {
                    Debug.LogError("Can't calculate cell desired cell size properly");
                    return new Vector2(calculatedCellWidth, calculatedCellHeight);
                }
            }
        }
        else
        {
            cellWidth = fitX
                ? calculatedCellWidth
                : cellSize.x;
            cellHeight = fitY
                ? calculatedCellHeight
                : cellSize.y;
        }

        return new Vector2(
            cellWidth,
            cellHeight
        );
    }

    private bool TryFitCellByWidth(float calculatedCellWidth, float calculatedCellHeight, 
        out float cellWidth, out float cellHeight)
    {
        var desiredCellHeight = calculatedCellWidth / aspectRatio;
        if (desiredCellHeight > calculatedCellHeight)
        {
            cellWidth = default;
            cellHeight = default;
            return false;
        }

        cellWidth = calculatedCellWidth;
        cellHeight = desiredCellHeight;
        return true;
    }

    private bool TryFitCellByHeight(float calculatedCellWidth, float calculatedCellHeight,
        out float cellWidth, out float cellHeight)
    {
        var desiredCellWidth = calculatedCellHeight * aspectRatio;
        if (desiredCellWidth > calculatedCellWidth)
        {
            cellWidth = default;
            cellHeight = default;
            return false;
        }

        cellWidth = desiredCellWidth;
        cellHeight = calculatedCellHeight;
        return true;
    }

    private void UpdateChildren(Vector2 currentCellSize)
    {
        var paddingShift = new Vector2(
            padding.left,
            padding.top
        );
        for (int i = 0; i < rectChildren.Count; i++)
        {
            var childGridPos = new Vector2Int(
                i % columns,
                i / columns
            );
            var spacingShift = childGridPos * spacing;
            var childPos = currentCellSize * childGridPos + spacingShift + paddingShift;
            var child = rectChildren[i];

            SetChildAlongAxis(child, 0, childPos.x, currentCellSize.x);
            SetChildAlongAxis(child, 1, childPos.y, currentCellSize.y);
        }
    }

    public override void CalculateLayoutInputVertical()
    { }

    public override void SetLayoutHorizontal()
    { }

    public override void SetLayoutVertical()
    { }
}
}