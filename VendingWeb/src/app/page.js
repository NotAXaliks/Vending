"use client";

import { redirect } from "next/navigation";
import MachinesPage from "./machines/page";

export default function Home() {
  redirect("/machines");

  return (
    <MachinesPage />
  );
}
