"use client"

import Link from "next/link";
import Cabecalho from "../cabecalho/page";
import { useEffect, useState } from "react";
import MaskedInput from 'react-text-mask';

export default function Novo() {

    const [errorItem, seterrorItem] = useState(false);
    const [stateEmpresa, setStateEmpresa] = useState([]);
    const [stateSetor, setStateSetor] = useState([]);
    const [stateItem, setStateItem] = useState({
        empresa_id: "",
        setor_id: ""
    });

    useEffect(() => {
        fetch("http://localhost:5064/api/empresa")
        .then(r => r.json())
        .then(r =>{
            setStateEmpresa(r);
            console.log(r);
        });

        fetch("http://localhost:5064/api/setor")
        .then(r => r.json())
        .then(r =>{
            setStateSetor(r);
            console.log(r);
        });
    }, []);

    const cnpjmask = [/[1-9]/,/[1-9]/," ", /[1-9]/,/[1-9]/,/[1-9]/," ", /[1-9]/,/[1-9]/,/[1-9]/,"/","0","0","0","1","-",/[1-9]/,/[1-9]/];

    async function handleNovoVinculo(){
        seterrorItem(false);
        console.log(stateItem.empresa_id);
        console.log(stateItem.setor_id);
        const requestOptions = {
            method: 'POST',
            body: JSON.stringify(stateItem),
            headers: new Headers({
            'Content-Type': 'application/json',
            'Accept': 'application/json'
            }),
        };

            const response = await fetch('http://localhost:5064/api/vincular', requestOptions);
            if(response.status == 400){
                seterrorItem(true);
            }
            else{
                alert("Vinculo Empresa-Setor Criado Com Sucesso")
                setStateItem({
                    empresa_id: "",
                    setor_id: ""
                })
            }
        console.log(response);
    }



    
  return (
    <div>
      <Cabecalho></Cabecalho>
      <div className="principal">
        <h1 className="border-bottom pb-3 ml-5 mb-3 mr-5">Vinculo de Empresas e Setores</h1>
        <div className="container w-100">
            <div className="flexcontainer w-100">
                <div class="form-body">
                    <label for="razao_social" class="form-label">Id Empresa</label>
                    <div class="input-group mb-3">
                        <select value={stateItem.empresa_id} onChange={(e)=> setStateItem((stateItem) => ({...stateItem, empresa_id: e.target.value}))} type="text" class="form-control" id="razao_social">
                            {stateEmpresa.map(sel =>
                            <option key={sel.id} value={sel.id}>{sel.razao_social}</option>
                            )};
                        </select>
                    </div>

                    <label for="nome_fantasia" class="form-label">Id Setor</label>
                    <div class="input-group mb-3">
                        <select value={stateItem.setor_id} onChange={(e)=> setStateItem((stateItem) => ({...stateItem, setor_id: e.target.value}))} type="text" class="form-control" id="nome_fantasia">
                            {stateSetor.map(sel =>
                            <option key={sel.id} value={sel.id}>{sel.descricao}</option>
                            )};
                        </select>
                    </div>

                </div>
                <div class="form-footer">
                    <button onClick={() => handleNovoVinculo()} type="button" class="btn btn-primary">Criar</button>
                </div>
                {errorItem ?<p class="mt-3 text-danger">Vinculo já existente</p> : <></>}
            </div>
        </div>
      </div>
      
    </div>
  );
}
