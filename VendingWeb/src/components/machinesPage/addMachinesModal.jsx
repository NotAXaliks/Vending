import { useState } from "react";
import { Modal } from "../modal";

function AddMachinesModal({ isOpen, onClose }) {
  const [message, setMessage] = useState("");

  const parseCsvFromStream = (stream) => {
    return new Promise((resolve, reject) => {

    });
  }

  const handleFileSelect = async (event) => {
    const [file] = event.target.files;
    if (!file) return;

    if (file.size > 1024 * 1024 * 1024) return setMessage("Максимальный размер файла: 1ГБ");

    setMessage(`Загружаем... ${file.name}`);
    console.log(file);
  
    try {
      const data = await parseCsvFromStream(file.stream());
      console.log(data);

      return setMessage(`Успешно загружен! ${file.name}`);
    } catch (error) {
      console.error("Error uploading file:", error);
      return setMessage("Ошибка при загрузке файла");
    }
  };

  return (
    <Modal name="Добавить аппараты" open={isOpen} onClose={onClose}>
      <form className="machineFileUpload">
        <input
          id="file"
          type="file"
          name="file"
          className="fileInput"
          onChange={handleFileSelect}
        />
        <label htmlFor="file">
          <p>{message || "Загрузите файл .csv"}</p>
        </label>
      </form>
    </Modal>
  );
}

export { AddMachinesModal };
