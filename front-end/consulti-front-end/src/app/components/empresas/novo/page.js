"use client"

import Link from "next/link";
import Cabecalho from "../../cabecalho/page";
import { useEffect, useState } from "react";
import MaskedInput from 'react-text-mask';

export default function Novo() {

    const [errorItem, seterrorItem] = useState(false);

    const [stateItem, setStateItem] = useState({
        razao_social: "",
        nome_fantasia: "",
        cnpj: ""
    });

    const cnpjmask = [/[1-9]/,/[1-9]/," ", /[1-9]/,/[1-9]/,/[1-9]/," ", /[1-9]/,/[1-9]/,/[1-9]/,"/","0","0","0","1","-",/[1-9]/,/[1-9]/];

    async function handleNovo(){
        seterrorItem(false);
        const requestOptions = {
            method: 'POST',
            body: JSON.stringify(stateItem),
            headers: new Headers({
            'Content-Type': 'application/json',
            'Accept': 'application/json'
            }),
        };

            const response = await fetch('http://localhost:5064/api/empresa', requestOptions);
            if(response.status == 400){
                seterrorItem(true);
            }
            else{
                alert("Empresa Criada Com Sucesso")
                setStateItem({
                    razao_social: "",
                    nome_fantasia: "",
                    cnpj: ""
                })
            }
        console.log(response);
    }



    
  return (
    <div>
      <Cabecalho></Cabecalho>
      <div className="principal">
        <h1 className="border-bottom pb-3 ml-5 mb-3 mr-5">Gerenciamento de Empresas</h1>
        <div className="container w-100">
            <div className="flexcontainer w-100">
                <div class="form-body">
                    <label for="razao_social" class="form-label">Razao Social</label>
                    <div class="input-group mb-3">
                        <input value={stateItem.razao_social} onChange={(e)=> setStateItem((stateItem) => ({...stateItem, razao_social: e.target.value}))} type="text" class="form-control" id="razao_social"/>
                    </div>

                    <label for="nome_fantasia" class="form-label">Nome Fantasia</label>
                    <div class="input-group mb-3">
                        <input value={stateItem.nome_fantasia} onChange={(e)=> setStateItem((stateItem) => ({...stateItem, nome_fantasia: e.target.value}))} type="text" class="form-control" id="nome_fantasia"/>
                    </div>

                    <label for="cnpj" class="form-label">CNPJ</label>
                    <div class="input-group mb-3">
                        <MaskedInput mask={cnpjmask} value={stateItem.cnpj} onChange={(e)=> setStateItem((stateItem) => ({...stateItem, cnpj: e.target.value}))} type="text" class="form-control" id="cnpj"/>
                    </div>
                </div>
                <div class="form-footer">
                    <button onClick={() => handleNovo()} type="button" class="btn btn-primary">Criar</button>
                </div>
                {errorItem ?<p class="mt-3 text-danger">Formulário incorreto</p> : <></>}
            </div>
        </div>
      </div>
      
    </div>
  );
}
