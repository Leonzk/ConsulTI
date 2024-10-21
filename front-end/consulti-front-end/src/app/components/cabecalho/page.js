"use client"

import "./page.css"
import Link from "next/link";

export default function Cabecalho() {
  return (
    <nav class="navbar navbar-expand-md navbar-dark bg-dark fixed-top">
        <Link class="nav-link navbar-brand" href="/">ConsulTI</Link>
      <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarsExampleDefault" aria-controls="navbarsExampleDefault" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>

      <div class="collapse navbar-collapse" id="navbarsExampleDefault">
        <ul class="navbar-nav mr-auto">
          <li class="nav-item active">
            <Link class="nav-link" href="/">Home <span class="sr-only">(current)</span></Link>
          </li>
          <li class="nav-item">
            <Link class="nav-link" href="/components/empresas">Empresas</Link>
          </li>
          <li class="nav-item">
            <Link class="nav-link" href="/components/setores">Setores</Link>
          </li>
          <li class="nav-item">
            <Link class="nav-link" href="/components/empresa_setor">Vincular Empresa-Setor</Link>
          </li>
        </ul>
      </div>
    </nav>
  );
}
