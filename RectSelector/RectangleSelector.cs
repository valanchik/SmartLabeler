
using Hotkeys;
using InputControllers;
using ProcScan.RectSelector;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RectSelector
{

    public class RectangleSelector : IScalible
    {
        public delegate void RectangleHandler(ResizableRectangle clickedRectangle);
        public event RectangleHandler RectangleClicked;
        public event RectangleHandler RectangleSelected;
        private readonly PictureBox pictureBox;
        private readonly Label rectInfo;
        private readonly Button addRectToFrame;
        private int _selectedHandle;
        private ResizableRectangle _resizableRect;
        private List<ResizableRectangle> _resizableRectangles;
        private ResizableRectangle _selectedResizableRect;
        private ResizableRectangleManager _resizableRectangleManager;
        private readonly DrawingRectangle _drawingRectangle;
        private RectangleMover _rectangleMover;
        private double scalingFactor = 1F;
        private readonly IInputController inputController;
        private readonly HotkeyManager hotkeyManager;
        private ResizableRectangle clickedResizableRectangle;
        private ResizableRectangle forCopyRectangle;


        public RectangleSelector(PictureBox pictureBox, IInputController inputController, HotkeyManager hotkeyManager)
        {
            this.pictureBox = pictureBox;
            this.inputController = inputController;
            this.hotkeyManager = hotkeyManager;
            rectInfo = (Label)inputController.GetElement(InputsControllerType.RectangleInfo);
            addRectToFrame = ((Button)this.inputController.GetElement(InputsControllerType.AddRectToFrameBtn));
            InitializeEventHandlers();
            _resizableRectangles = new List<ResizableRectangle>();
            _rectangleMover = new RectangleMover(_resizableRect);
            _resizableRectangleManager = new ResizableRectangleManager();
            _drawingRectangle = new DrawingRectangle();
            ResetState();
            hotkeyManager.AddHotkey(new Hotkey(Keys.C, true, false,false), OnCopy);
            hotkeyManager.AddHotkey(new Hotkey(Keys.V, true,false,false), OnPaste);
            hotkeyManager.AddHotkey(new Hotkey(Keys.Delete), OnDelete);
        }

        private void OnDelete()
        {
            
        }

        private void OnPaste()
        {
            if (forCopyRectangle!=null)
            {
                AddRectangle(forCopyRectangle);
            }
        }

        private void OnCopy()
        {
            if (clickedResizableRectangle!=null)
            {
                forCopyRectangle = clickedResizableRectangle;
            }
        }

        public void SetRectangles(List<ResizableRectangle> list)
        {
            foreach (ResizableRectangle rect in list) rect.ScaleFactor = scalingFactor;
            _resizableRectangles = list;
        }
        public void AddRectangle(ResizableRectangle rect)
        {
            var cloned = rect.Clone();
            cloned.SetScaleFactor(scalingFactor);
            cloned.Index = _resizableRectangles.Count;
            _resizableRectangles.Add(rect);
            
        }
        public PictureBox GetPictureBox()
        {
            return pictureBox;
        }
        public void SetScaleFactor(double scaleFactor)
        {
            if (scaleFactor > 0)
            {
                scalingFactor = scaleFactor;
                foreach (ResizableRectangle rect in _resizableRectangles)
                {
                    rect.SetScaleFactor(scalingFactor);
                }
                _drawingRectangle.SetScaleFactor(scalingFactor);
                _resizableRectangleManager?.SetScaleFactor(scalingFactor);
                UpdateAllRectangles();
            }
        }

        public bool IsAnyProcess()
        {
            return _rectangleMover.IsMoving || _resizableRectangleManager.IsResizing || _drawingRectangle.IsDrawing;
        }

        private void InitializeEventHandlers()
        {
            pictureBox.MouseDoubleClick += PictureBox_MouseDoubleClick;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseUp += PictureBox_MouseUp;
            pictureBox.Paint += PictureBox_Paint;
            addRectToFrame.Click += ClickOnNewRect;
            pictureBox.MouseClick += PictureBox_MouseClick;
        }
        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            (ResizableRectangle _selectedResizableRect, int handleIndex) = GetSelectedResizableRectangle(e.Location);
            if (_selectedResizableRect != null)
            {
                clickedResizableRectangle = _selectedResizableRect;
            }
            else
            {
                clickedResizableRectangle = null;
            }
            RectangleClicked?.Invoke(clickedResizableRectangle);
        }

        private void ResetState()
        {
            _drawingRectangle.StopDrawing();
            _resizableRectangleManager?.StopResizing();
            _selectedHandle = -1;
        }

        private void ClickOnNewRect(object sender, EventArgs e)
        {
            _resizableRect = new ResizableRectangle(_resizableRectangles.Count, pictureBox);
            _resizableRect.SetScaleFactor(scalingFactor);
            _resizableRectangles.Add(_resizableRect);
            _drawingRectangle.SetResizebleRectangle(_resizableRect);
            _drawingRectangle.IsDrawing = true;
            addRectToFrame.Enabled = false;
        }

        private void PictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnDelete();
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
                    RectangleSelected?.Invoke(_selectedResizableRect);
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

            foreach (ResizableRectangle rect in _resizableRectangles)
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
            _selectedResizableRect.SetScaleFactor(scalingFactor);
            _resizableRectangleManager.StartResizing(_selectedHandle, location);
        }

        private void StartMoving(Point location)
        {
            _rectangleMover = new RectangleMover(_selectedResizableRect);
            //KeepRectangleInsidePictureBox()
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
            rectInfo.Text = $"{e.Location}";

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

                pictureBox.Cursor = cursor;
                if (selectedRect != null)
                {
                    selectedRect.SetDrawHandleStatus(true);
                    List<ResizableRectangle> list = _resizableRectangles.FindAll(x => x.Index != selectedRect.Index);
                    foreach (ResizableRectangle rect in list)
                    {
                        rect.SetDrawHandleStatus(false);
                    }
                }
                else
                {
                    foreach (ResizableRectangle rect in _resizableRectangles)
                    {
                        rect.SetDrawHandleStatus(false);
                    }
                }
                _selectedResizableRect = selectedRect;
            }
        }
        private (Cursor, ResizableRectangle) GetCursorForLocation(Point location)
        {
            // location уже умножен
            ResizableRectangle selectedRect = null;
            //var forIsmouseinside = new Point((int)(location.X / _scalingFactor), (int)(location.Y / _scalingFactor));
            foreach (ResizableRectangle rect in _resizableRectangles)
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
            if (_resizableRect == null) return false;
            return _resizableRect.IsMouseInsideRectangle(location);
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            foreach (ResizableRectangle rect in _resizableRectangles)
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
            addRectToFrame.Enabled = true;
            UpdateAllRectangles();
        }

        private void UpdateLabel()
        {
            if (_resizableRect == null) return;
            Rectangle rect = _resizableRect.GetRectangle();
            rectInfo.Text = $"X: {rect.X}, Y: {rect.Y}, Ширина: {rect.Width}, Высота: {rect.Height}";
        }
        public void UpdateAllRectangles()
        {
            foreach (ResizableRectangle rect in _resizableRectangles)
            {
                rect.UpdateHandles();
            }
            pictureBox.Invalidate();
        }

    }
}
