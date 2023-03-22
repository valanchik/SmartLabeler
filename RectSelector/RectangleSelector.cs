﻿
using ProcScan.RectSelector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace RectSelector
{

    public class RectangleSelector :IScalible
    {
        
        private bool _isResizing;
        private readonly PictureBox _pictureBox;
        private readonly Label _label;
        private readonly Button _button;
        private int _selectedHandle;

        private ResizableRectangle _resizableRect;
        private List<ResizableRectangle> _resizableRectangles;
        private ResizableRectangle _selectedResizableRect;
        private ResizableRectangleManager _resizableRectangleManager;
        private DrawingRectangle _drawingRectangle;
        private RectangleMover _rectangleMover;
        private double _scalingFactor = 1F;

        public RectangleSelector(PictureBox pictureBox, Label label, Button button)
        {
            _pictureBox = pictureBox;
            _label = label;
            _button = button;
            InitializeEventHandlers();
            _resizableRect = new ResizableRectangle();
            _resizableRectangles = new List<ResizableRectangle>();
            _rectangleMover = new RectangleMover(_resizableRect);
            _resizableRectangleManager = new ResizableRectangleManager();
            _drawingRectangle = new DrawingRectangle();
            ResetState();
        }
        public void SetScaleFactor(double scaleFactor)
        {
            if (scaleFactor > 0)
            {
                _scalingFactor = scaleFactor;
                foreach (var rect in _resizableRectangles) {
                    rect.SetScaleFactor(_scalingFactor);
                }
                _drawingRectangle.SetScaleFactor(_scalingFactor);
                _resizableRectangleManager?.SetScaleFactor(_scalingFactor);
                UpdateAllRectangles();
            }
           
        }

        public bool IsAnyProcess()
        {
            return _rectangleMover.IsMoving || _resizableRectangleManager.IsResizing || _drawingRectangle.IsDrawing;
        }

        private void InitializeEventHandlers()
        {
            _pictureBox.MouseDoubleClick += PictureBox_MouseDoubleClick;
            _pictureBox.MouseMove += PictureBox_MouseMove;
            _pictureBox.MouseDown += PictureBox_MouseDown;
            _pictureBox.MouseUp += PictureBox_MouseUp;
            _pictureBox.Paint += PictureBox_Paint;
            _button.Click += Button_Click;
        }

        private void ResetState()
        {
            _drawingRectangle.StopDrawing();
            _resizableRectangleManager?.StopResizing();
            _selectedHandle = -1;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            _resizableRect = new ResizableRectangle();
            _resizableRect.SetScaleFactor(_scalingFactor);
            _resizableRectangles.Add(_resizableRect);
            _drawingRectangle.SetResizebleRectangle(_resizableRect);
            _drawingRectangle.IsDrawing = true;
            _button.Enabled = false;
        }

        private void PictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            for (int i = _resizableRectangles.Count - 1; i >= 0; i--)
            {
                if (_resizableRectangles[i].IsMouseInsideRectangle(e.Location))
                {
                    _resizableRectangles.RemoveAt(i);
                    _pictureBox.Invalidate();
                    break;
                }
            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (_drawingRectangle.IsDrawing)
            {
                StartDrawing(e.Location);
            }
            else if (!_resizableRectangleManager.IsResizing)
            {
                ProcessSelectionAndResizing(e.Location);
            }
        }


        private void ProcessSelectionAndResizing(Point location)
        {
            (ResizableRectangle _selectedResizableRect, int handleIndex) = GetSelectedResizableRectangle(location);

            if (_selectedResizableRect != null)
            {
                if (handleIndex != -1)
                {
                    this._selectedResizableRect = _selectedResizableRect;
                    StartResizing(location, handleIndex);
                }
                else if (!_rectangleMover.IsMoving)
                {
                    StartMoving(location);
                }
            }
        }

        private (ResizableRectangle, int) GetSelectedResizableRectangle(Point location)
        {
            int handleIndex = -1;
            ResizableRectangle selectedRect = null;

            foreach (var rect in _resizableRectangles)
            {
                handleIndex = rect.GetSelectedHandle(location);
                if (handleIndex != -1 || rect.IsMouseInsideRectangle(location))
                {
                    selectedRect = rect;
                    break;
                }
            }

            return (selectedRect, handleIndex);
        }

        private void StartResizing(Point location, int handleIndex)
        {
            _selectedHandle = handleIndex;
            _resizableRectangleManager = new ResizableRectangleManager();
            _resizableRectangleManager.SetResizableRectangle(_selectedResizableRect);
            _selectedResizableRect.SetScaleFactor(_scalingFactor);
            _resizableRectangleManager.StartResizing(_selectedHandle, location);
        }

        private void StartMoving(Point location)
        {
            _rectangleMover = new RectangleMover(_selectedResizableRect);
            _rectangleMover.StartMoving(location);
        }

        

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (_drawingRectangle.IsDrawing)
            {
                StopDrawing();
            }

            if (_resizableRectangleManager?.IsResizing == true)
            {
                _resizableRectangleManager.StopResizing();
            }

            if (_rectangleMover.IsMoving)
            {
                _rectangleMover.StopMoving();
            }

            UpdateLabel();
        }
        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            _label.Text = $"{e.Location}";

            if (_drawingRectangle.IsDrawing && e.Button == MouseButtons.Left)
            {
                _drawingRectangle.UpdateRectangleSize(e.Location);
                UpdateAllRectangles();
            }
            else if (_resizableRectangleManager?.IsResizing == true && e.Button == MouseButtons.Left)
            {
                _resizableRectangleManager.Resize(e.Location);
                UpdateAllRectangles();
            }
            else if (_rectangleMover?.IsMoving == true && e.Button == MouseButtons.Left)
            {
                _rectangleMover.Move(e.Location);
                UpdateAllRectangles();
            }
            else
            {
                UpdateAllRectangles();
                (Cursor cursor, ResizableRectangle selectedRect) = GetCursorForLocation(e.Location);

                _pictureBox.Cursor = cursor;
                if (selectedRect!=null) {
                    selectedRect.SetDrawHandleStatus(true);
                } else
                {
                    _selectedResizableRect?.SetDrawHandleStatus(false);
                }
                _selectedResizableRect = selectedRect;
            }
        }

        private (Cursor, ResizableRectangle) GetCursorForLocation(Point location)
        {
            // location уже умножен
            ResizableRectangle selectedRect = null;
            //var forIsmouseinside = new Point((int)(location.X / _scalingFactor), (int)(location.Y / _scalingFactor));
            foreach (var rect in _resizableRectangles)
            {
                int handleIndex = rect.GetSelectedHandle(location); // надо умноженный
                if (handleIndex > -1)
                {
                    return (rect.GetCursorForHandle(handleIndex), rect);
                }
                else if (rect.IsMouseInsideRectangle(location)) // надо базовый
                {
                    selectedRect = rect;
                }
            }

            return (selectedRect != null ? Cursors.SizeAll : Cursors.Default, selectedRect);
        }

        public bool IsMouseInsideRectangle(Point location)
        {
            return _resizableRect.IsMouseInsideRectangle(location);
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            foreach (var rect in _resizableRectangles)
            {
                rect.DrawRectangleAndHandles(e.Graphics);
            }
        }

        private void StartDrawing(Point startPoint)
        {
            _drawingRectangle.StartDrawing(startPoint);
            UpdateAllRectangles();
        }

        private void StopDrawing()
        {
            _drawingRectangle.StopDrawing();
            _button.Enabled = true;
            UpdateAllRectangles();
        }

        private void UpdateLabel()
        {
            Rectangle rect = _resizableRect.GetRectangle();
            _label.Text = $"X: {rect.X}, Y: {rect.Y}, Ширина: {rect.Width}, Высота: {rect.Height}";
        }
        public void UpdateAllRectangles()
        {
            foreach (var rect in _resizableRectangles)
            {
                rect.UpdateHandles();
            }
            _pictureBox.Invalidate();
        }
        
    }
}
