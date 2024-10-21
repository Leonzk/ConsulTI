"use client"

import "./page.css"
import Link from "next/link";
import Cabecalho from "../../cabecalho/page";
import { useEffect, useState } from "react";
import MaskedInput from 'react-text-mask';

export default function Novo() {

    const [errorItem, seterrorItem] = useState(false);

    const [stateItem, setStateItem] = useState({
        descricao: ""
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

            const response = await fetch('http://localhost:5064/api/setor', requestOptions);
            if(response.status == 400){
                seterrorItem(true);
            }
            else{
                alert("Empresa Criada Com Sucesso")
                setStateItem({
                    descricao: ""
                })
            }
        console.log(response);
    }



    
  return (
    <div>
      <Cabecalho></Cabecalho>
      <div className="principal">
        <h1 className="border-bottom pb-3 ml-5 mb-3 mr-5">Gerenciamento de Setores</h1>
        <div className="container w-100">
            <div className="flexcontainer w-100">
                <div class="form-body">
                    <label for="descricao" class="form-label">Descrição do Setor</label>
                    <div class="input-group mb-3">
                        <input value={stateItem.descricao} onChange={(e)=> setStateItem((stateItem) => ({...stateItem, descricao: e.target.value}))} type="text" class="form-control" id="Título"/>
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
