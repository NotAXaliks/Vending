function Modal({ open, name = "Name", style, children, onClose }) {
  if (!open) return null;

  return (
    <>
      <div className="modalOverlay" onClick={onClose} />
      <div className="modal" style={style}>
        <div className="modalHeader">
          <span className="modalTitle">{name}</span>
          <span className="modalClose" onClick={onClose}>×</span>
        </div>

        <div className="modalContent">
          {children}
        </div>
      </div>
    </>
  );
}

export { Modal };
