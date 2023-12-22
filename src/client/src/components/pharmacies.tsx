import React, { useEffect, useState } from 'react';
import { Table, Button, Loader} from 'semantic-ui-react'
import { apiService } from '../api/apiService';
import { Pharmacy } from '../api/types';
import PharmacyModal from './PharmacyModal'
import './pharmacies.css';

const Pharmacies = () => {

    const [pageNumber, setPageNumber] = useState(1)
    const [postsPerPage, setPostsPerPage] = useState(5)
    const[isLoading, setIsLoading] = useState(false);
    const[pharmacyList, setPharmacyList] = useState<Pharmacy[]|null>(null);
    const[selectedPharmacy, setSelectedPharmacy] = useState<Pharmacy|null>(null);

    
    useEffect(() => {
        const loadDataAsync = async () => {
            try {
                setIsLoading(true);
                const apiData = await apiService.getPharmacyList(pageNumber, postsPerPage);
                setPharmacyList(apiData);
            }
            catch (error) {
                console.error("Failed to load data", error);
            }
            finally {
                setIsLoading(false);
            }
        }
        loadDataAsync();
    }, []);   
    
    const handleEdit = (pharmacy: Pharmacy) => {
        setSelectedPharmacy(pharmacy);
    }

    const handleSave = (updatedPharmacy: Pharmacy) => {
        var idx = pharmacyList?.findIndex(x => x.id === updatedPharmacy.id);
        if (pharmacyList !== null && idx !== undefined) {
            pharmacyList[idx] = updatedPharmacy;
        }

    }

    return (
        <>
        <div id="pharmacy-container">
        <Loader>Loading</Loader>
        <Table unstackable sortable selectable celled fixed className='pharmacy-table'>
            <Table.Header>
                <Table.Row>
                    <Table.HeaderCell>Name</Table.HeaderCell>
                    <Table.HeaderCell>Address</Table.HeaderCell>
                    <Table.HeaderCell>City</Table.HeaderCell>
                    <Table.HeaderCell>State</Table.HeaderCell>
                    <Table.HeaderCell>Zip Code</Table.HeaderCell>
                    <Table.HeaderCell>Fill Count</Table.HeaderCell>
                    <Table.HeaderCell></Table.HeaderCell>
                </Table.Row>
           </Table.Header>
           <Table.Body>
            {pharmacyList != null && pharmacyList.length > 0 ? pharmacyList.map((row: Pharmacy, index: number) => {
                return <Table.Row key={index} id="tr">
                    <Table.Cell>{row.name}</Table.Cell>
                    <Table.Cell>{row.address}</Table.Cell>
                    <Table.Cell>{row.city}</Table.Cell>
                    <Table.Cell>{row.state}</Table.Cell>
                    <Table.Cell>{row.zip}</Table.Cell>
                    <Table.Cell>{row.filledPrescriptionsCount}</Table.Cell>                    
                    <Table.Cell>
                        <Button icon='edit'
                        value={index}
                        onClick={() => handleEdit(row)} 
                        content='Edit'
                        />                            
                    </Table.Cell>
                </Table.Row>
                }) : ""
            }
           </Table.Body>
        </Table>
        {selectedPharmacy && <PharmacyModal pharmacy={selectedPharmacy} closeModal={() => setSelectedPharmacy(null)} savePharmacy={handleSave} />
        }
        </div>
        </>

    )

}

export default Pharmacies