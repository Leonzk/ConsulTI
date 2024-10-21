"use client"

import "./page.css"
import Link from "next/link";
import Cabecalho from "./components/cabecalho/page";

export default function Home() {
  return (
    <main className={"principal"}>
      <Cabecalho></Cabecalho>
    </main>
  );
}
