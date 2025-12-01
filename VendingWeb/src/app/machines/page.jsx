"use client";

import { useState } from "react";

export default function Page() {
  const [message, setMessage] = useState("");

  const uploadFile = async (formData) => {
    const file = formData.get("file");
    if (!file) return { error: "No file selected." };

    try {
      console.log(`Uploading file: ${file.name}, size: ${file.size} bytes`);

      const fileContent = Buffer.from(await file.arrayBuffer());
      console.log(fileContent.length);

      return { success: `File "${file.name}" uploaded successfully!` };
    } catch (error) {
      console.error("Error uploading file:", error);
      return { error: "Failed to upload file." };
    }
  };

  async function handleSubmit(event) {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);
    const result = await uploadFile(formData);

    if (result.success) setMessage(result.success);
    else if (result.error) setMessage(result.error);
  }

  return (
    <div className="machinesPage">
      <div className="machineFileUploadContainer">
        <form onSubmit={handleSubmit} className="machineFileUpload">
          <input type="file" name="file" />
          <button type="submit">Upload</button>
          {message && <p>{message}</p>}
        </form>
      </div>
    </div>
  );
}
