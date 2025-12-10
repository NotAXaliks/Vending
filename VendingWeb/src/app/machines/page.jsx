"use client";

import { Button } from "@/components/button";
import { AddMachinesModal } from "@/components/machinesPage/addMachinesModal";
import { MachinesTable } from "@/components/machinesPage/table";
import { useState } from "react";

export default function Page() {
  const [uploadFileModalOpen, setUploadFileModalOpen] = useState(false);

  return (
    <div className="machinesPage">
      <div className="machinesTable">
        <div className="machinesPageActions">
          <Button
            className={"machinesPageAddButon"}
            text={"Добавить"}
            onClick={() => setUploadFileModalOpen(true)}
          />
          {uploadFileModalOpen && (
            <AddMachinesModal
              isOpen={true}
              onClose={() => setUploadFileModalOpen(false)}
            />
          )}
        </div>

        <MachinesTable />
      </div>
    </div>
  );
}
