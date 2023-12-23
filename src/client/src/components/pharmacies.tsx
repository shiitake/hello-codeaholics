import { useEffect, useState } from 'react';
import ReactPaginate from 'react-paginate';
import { Table, Button, Loader, Dropdown} from 'semantic-ui-react'
import { apiService } from '../api/apiService';
import { Pharmacy } from '../api/types';
import PharmacyModal from './PharmacyModal'
import '../styles/pharmacies.css';
import '../styles/pagination.css';

const Pharmacies = () => {

    type PageChange = {
        selected: number
    }

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

    const pageCount: number = pharmacyList ? Math.ceil(pharmacyList?.length / postsPerPage) : 0;
    const changePage = ({ selected }: PageChange) => {
        console.log('page number: ', selected);
        setPageNumber(selected)
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
                    <Table.HeaderCell>Edit</Table.HeaderCell>
                </Table.Row>
           </Table.Header>
           <Table.Body>
            {pharmacyList != null && pharmacyList.length > 0 ? pharmacyList.slice(pageNumber, pageNumber + postsPerPage).map((row: Pharmacy, index: number) => {
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
                }) : null
            }
           </Table.Body>
        </Table>
        <div className="paginateandcount">
            <ReactPaginate
                pageCount={pageCount}
                onPageChange={changePage}
                containerClassName={"paginationBttns"}
                />
                <span className="paginationPostCount">
                    <Dropdown
                        options={[3, 25, 50, 100].map((item: number, index: number) => ({ key: index, text: item, value: item, }))}
                        onChange={(e, data) => { setPostsPerPage(Number(data.value))}}
                        value={postsPerPage} 
                    />
                    {'  '} Posts per page
                </span>
                
        </div>
        {selectedPharmacy && <PharmacyModal pharmacy={selectedPharmacy} closeModal={() => setSelectedPharmacy(null)} savePharmacy={handleSave} />
        }
        </div>
        </>

    )

}

export default Pharmacies